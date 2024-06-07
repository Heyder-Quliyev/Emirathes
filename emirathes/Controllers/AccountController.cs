using DocuSign.eSign.Model;
using emirathes.Extensions;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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


        //#region SocialMediaOperations

        //public IActionResult FacebookLogin(string returnUrl)
        //{
        //    string redirectnUrl = Url.Action("FacebookResponse","Account", new { returnUrl = returnUrl });
        //    var properties =_signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectnUrl);
        //    return new ChallengeResult("Facebook", properties);
        //}

        //public async Task<IActionResult> FacebookResponse(string returnUrl)
        //{
        //    var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Signup");
        //    }
        //    else
        //    {
        //        var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
        //        if (result.Succeeded)
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            if (loginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
        //            {
        //                ProgramUsers programUsers = new ProgramUsers()
        //                {
        //                    Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
        //                    UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name)

        //                };

        //            }

        //            return RedirectToAction("Signup");
        //        }
        //    }
        //    }
        //}
        //#endregion



        #region SocialMediaOperations

        public IActionResult FacebookLogin(string returnUrl)
        {
            string redirectnUrl = Url.Action("FacebookResponse", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectnUrl);
            return new ChallengeResult("Facebook", properties);
        }

        public async Task<IActionResult> FacebookResponse(string returnUrl)
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Signup");
            }
            else
            {
                var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                if (result.Succeeded)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    if (loginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        ProgramUsers programUsers = new ProgramUsers()
                        {
                            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name)
                        };
                        // You might want to add code here to save the programUsers to the database or perform other operations.

                        var createResult = await _userManager.CreateAsync(programUsers);
                        if (createResult.Succeeded)
                        {
                            var identityLogin = await _userManager.AddLoginAsync(programUsers, loginInfo);
                            if (identityLogin.Succeeded)
                            {
                                await _signInManager.SignInAsync(programUsers, true);
                                return Redirect(returnUrl);
                            }
                        }



                    }
                    // Optionally, redirect or return a view if the external login failed.




                    return RedirectToAction("Index", "Home");
                }
            }
        }

        #endregion



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
