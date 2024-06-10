using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MimeKit;
using emirathes.Extensions;
using emirathes.Models;
using System.Data;
using emirathes.ViewModels;


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


        public IActionResult Login()
        {
            return View();
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
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Password or email incorrect");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "User not found");
        //    }
        //    //emailService.SendEmailAsync();
        //    return View(model);
        //}













        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(loginVM.Email);
            if (user == null)
            {
                // Log that the user was not found
                Console.WriteLine("User not found.");
                // Optionally, you could return an error message to the user
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginVM);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
                return RedirectToAction("Confirmation", "Account");
            }

            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, true);
            if (result.Succeeded)
            {
                Console.WriteLine("Login succeeded.");
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                // Log that the user is locked out
                Console.WriteLine("User is locked out.");
                // Optionally, you could return a lockout message to the user
                return View("Lockout");
            }
            else
            {
                // Log that the login attempt failed
                Console.WriteLine("Invalid login attempt.");
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginVM);
            }
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ProgramUsers programUsers = new ProgramUsers()
                {
                    UserName = registerVM.Username,
                    Email = registerVM.Email,
                    
                };
                var result = await _userManager.CreateAsync(programUsers, registerVM.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(programUsers);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = programUsers.Id, token = token }, Request.Scheme);
                    await _emailService.SendEmailAsync(registerVM.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

                    await _userManager.AddToRoleAsync(programUsers, "User");

                    return RedirectToAction("Confirmation", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(registerVM);
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                TempData["Message"] = "An email has been sent to your email address with instructions on how to reset your password.";
                return RedirectToAction("Message");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { email = email, token = token }, Request.Scheme);
            await _emailService.SendEmailAsync(email, "Reset Password", $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");

            TempData["Message"] = "An email has been sent to your email address with instructions on how to reset your password.";
            return RedirectToAction("Message");
        }




        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Error");
            }

            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Error");
                }
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
                    return View(model);
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your password has been reset successfully.";
                    return RedirectToAction("Message");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Message()
        {
            ViewBag.Message = TempData["Message"];
            return View();
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
            }











        }


    }
}
