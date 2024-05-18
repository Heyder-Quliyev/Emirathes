using emirathes.Migrations;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
//using emirathes.Extensions;

namespace emirathes.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContent appDbContent;
        private readonly UserManager<ProgramUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ProgramUsers> _signInManager;
        //private readonly IEmailService _emailService;


        public AccountController(AppDbContent _appDbContent, RoleManager<IdentityRole> roleManager, SignInManager<ProgramUsers> signInManager, UserManager<ProgramUsers> userManager/*, IEmailService emailService*/)
        {
            appDbContent = _appDbContent;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            //_emailService = emailService;
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProgramUsers programUsers = new ProgramUsers
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(programUsers, model.Password);
            if (result.Succeeded)
            {
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(programUsers);
                //var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = programUsers.Id, token = token }, Request.Scheme);
                //await _emailService.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");


               await _userManager.AddToRoleAsync(programUsers, "User");
                return RedirectToAction("Index", "Home"); // redirect to action`i tek yazma. Qabaginda return yaz hemishe
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Password or email incorrect");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }
            //emailService.SendEmailAsync();
            return View(model);
        }







        //[HttpPost]
        //public async Task<IActionResult> Login(LoginVM model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Something Went Wrong");
        //        return View(model);
        //    }

        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user != null)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", "Password or email incorrent");
        //        }
        //    }
        //    //emailService.SendEmailAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task SeedRoles()
        {
           if(! await _roleManager.RoleExistsAsync(roleName: "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "Admin"));
            }

            if (!await _roleManager.RoleExistsAsync(roleName: "User"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "User"));
            }
            }


        public async Task SeedAdmin()
        {
           
            }


















        }
}
