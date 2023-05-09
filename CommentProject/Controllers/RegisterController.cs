using CommentProject.EntityLayer.Concrete;
using CommentProject.Models.AppUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System.Runtime.CompilerServices;
using CommentProject.ValidationForModels;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace CommentProject.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            var appUser = new AppUser()
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Email = registerViewModel.Mail,
                UserName = registerViewModel.UserName,
                Image = "deneme"
            };
            if (registerViewModel.Password == registerViewModel.ConfirmPassword)
            {
                RegisterModelValidator wv = new RegisterModelValidator();
                FluentValidation.Results.ValidationResult validationResult = new FluentValidation.Results.ValidationResult();
                validationResult = wv.Validate(registerViewModel);
                if (validationResult.IsValid)
                {
                    var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "Yazar");
                        return RedirectToAction("EmailConfirm", "Register", new { id = appUser.Id });
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
						return View();
					}
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor!");
                return View();
            }
	
        }
        [HttpGet]
        public async Task<IActionResult> EmailConfirm(int? id)
        {
            var appUser = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (id == null)
            {
               appUser = _userManager.Users.FirstOrDefault(x => x.Id == (int)HttpContext.Session.GetInt32("LoggerID"));
            }
            TempData["EmailID"] = appUser.Id;
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("CommentProject", "commentproject60@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            Random rnd = new Random();
            int confirmCode = rnd.Next(100, 999);
            appUser.ConfirmNumber = confirmCode;
            await _userManager.UpdateAsync(appUser);
            bodyBuilder.TextBody = " E-Mail doğrulama kodunuz: " + confirmCode.ToString() + ".";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "E-Mail Doğrulama";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("commentproject60@gmail.com", "xsvtlmnxpsvsbnxv");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmailConfirm(EmailConfirmViewModel emailConfirm)
        {
            var appUser = _userManager.Users.FirstOrDefault(x => x.Id == (int)TempData["EmailID"]);
            if (appUser.ConfirmNumber == emailConfirm.ConfirmCode) 
            {
                appUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(appUser);
                return RedirectToAction("Index", "Login");
            }
            ModelState.AddModelError("ConfirmNumber", "Kod doğru değil. Lütfen kontrol ediniz.");
            return View();
        }
    }
}
