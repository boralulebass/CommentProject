using CommentProject.EntityLayer.Concrete;
using CommentProject.Models.AppUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;

namespace CommentProject.Controllers
{
    [Authorize(Roles = "Yazar")]
    public class WriterProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public WriterProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userinfo = new EditProfileViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            };
            return View(userinfo);
        }
        [HttpPost]
        public async Task<IActionResult> Index(EditProfileViewModel editProfile)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            appUser.Surname = editProfile.Surname;
            appUser.Name = editProfile.Name;
            appUser.Email = editProfile.Email;
            var result = _userManager.PasswordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, editProfile.Password);
            int sonuc = ((int)result);
            if (sonuc != 0)
            {

                await _userManager.UpdateAsync(appUser);
                return RedirectToAction("Index", "WriterProfile");
            }
            else
            {
                ModelState.AddModelError("Password", "Şifreniz Yanlış");
                return View();
            }
        }
        [HttpGet]
        public IActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditProfileViewModel editPassword)
        {
            if (editPassword.NewPassword == editPassword.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, editPassword.OldPassword);
                if (((int)result) != 0)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editPassword.NewPassword);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "WriterProfile");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Güncel şifreniz yanlış!");
                    return RedirectToAction("Index", "WriterProfile");
                }
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword", "Yeni şifreniz ve şifre onay kutucuğu aynı değil!");
                return RedirectToAction("Index", "WriterProfile");
            }
        }
        public async Task<IActionResult> DeleteUser()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index", "WriterCategory");

        }
        [AllowAnonymous]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == 0)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Name = " " + user.Name + " " + user.Surname;
                ViewBag.Mail = user.Email;
                ViewBag.Username = user.UserName;
                ViewBag.ID = user.Id;
                return View();
            }
            else 
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
                ViewBag.Name = " " + user.Name + " " + user.Surname;
                ViewBag.Mail = user.Email;
                ViewBag.Username = user.UserName;
                ViewBag.ID = user.Id;
                return View();
            }
        }
    }
}
