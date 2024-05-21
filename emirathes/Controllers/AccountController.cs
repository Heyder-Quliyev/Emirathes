using emirathes.Extensions;
using emirathes.Migrations;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace emirathes.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContent appDbContent;
        private readonly UserManager<ProgramUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ProgramUsers> _signInManager;
        private readonly IEmailService _emailService;


        public AccountController(AppDbContent _appDbContent, RoleManager<IdentityRole> roleManager, SignInManager<ProgramUsers> signInManager, UserManager<ProgramUsers> userManager, IEmailService emailService)
        {
            appDbContent = _appDbContent;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
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
            
            var programUsers = new ProgramUsers
            {
                Email = model.Email,
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(programUsers, model.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(programUsers, "User");
                await _signInManager.SignInAsync(programUsers, true);




                var token = await _userManager.GenerateEmailConfirmationTokenAsync(programUsers);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = programUsers.Id, token = token });
                //hemishe using de hansi datani ishletdiyine bax
                await _emailService.SendEmailAsync(model.Email!, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

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


        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if(Id == null || token == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View("Error");
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
          
          if (_userManager.FindByEmailAsync("heyderquliyev30@gmail.com").Result == null)
            {
                ProgramUsers programUser = new ProgramUsers
                {
                    Email = "heyderquliyev30@gmail.com",
                    UserName = "heyderquliyev30@gmail.com"
                };

                var result = await _userManager.CreateAsync(programUser, "77heyder77");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(programUser, "Admin");
                    await _signInManager.SignInAsync(programUser, true);

                    RedirectToAction("Index", "Home");
                }
                //IdentityResult result = await _userManager.CreateAsync(admin, "");
                //if (result.Succeeded)
                //{
                //    _userManager.AddToRoleAsync(admin, "Admin").Wait();
                    
                //}



                    }










        }


    }
}
