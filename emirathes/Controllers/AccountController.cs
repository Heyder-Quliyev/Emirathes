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
        private readonly SignInManager<ProgramUsers> _signInManager;
        public AccountController(AppDbContent _appDbContent , SignInManager<ProgramUsers> signInManager, UserManager<ProgramUsers> userManager)
        {
            appDbContent = _appDbContent;
            _signInManager = signInManager;
            _userManager = userManager;
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
                ModelState.AddModelError("", "Something incorrent");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Password or email incorrent");
                }
            }
            //_emailService.SendEmailAsync()
            return RedirectToAction("Index", "Home");
        }


        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

















    }
}
