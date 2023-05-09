using CommentProject.EntityLayer.Concrete;
using CommentProject.Models.AppUserViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace CommentProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                HttpContext.Session.SetInt32("LoggerID", user.Id);
                return RedirectToAction("PathFinder", "Login");
            }
            var oneUser = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (oneUser.EmailConfirmed == false)
            {
                return RedirectToAction("EmailConfirm", "Register", new { id = oneUser.Id });
            }
            else
            {
                return View();
            }

        }
        public IActionResult PathFinder()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Category");
            }
            if (User.IsInRole("Yazar"))
            {
                return RedirectToAction("Index", "WriterCategory");
            }
            return View();
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "WriterCategory");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            return RedirectToAction("ResetPassword", new { email = model.Email });
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
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
            bodyBuilder.TextBody = " Şifre sıfırlama kodunuz: " + confirmCode.ToString() + ".";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Şifre Sıfırlama";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("commentproject60@gmail.com", "xsvtlmnxpsvsbnxv");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(EmailConfirmViewModel emailConfirm)
        {
            var appUser = _userManager.Users.FirstOrDefault(x => x.Id == (int)TempData["EmailID"]);
            if (appUser.ConfirmNumber == emailConfirm.ConfirmCode)
            {
                return RedirectToAction("EditPassword", "Login");
            }
            ModelState.AddModelError("ConfirmNumber", "Kod doğru değil. Lütfen kontrol ediniz.");
            return View();
        }
        public IActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(ResetPasswordViewModel editPassword)
        {
            if(editPassword.PasswordNew == editPassword.PasswordNewConfirm)
            { 
            var user = _userManager.Users.FirstOrDefault(x => x.Id == (int)TempData["EmailID"]);
            var result = _userManager.PasswordHasher.HashPassword(user, editPassword.PasswordNew);
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Login");
            }
            ModelState.AddModelError("", "Şifreler Uyuşmuyor.");
            return View();
        }

    }
}
