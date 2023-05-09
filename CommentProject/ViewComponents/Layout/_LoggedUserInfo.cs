using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CommentProject.ViewComponents.Layout
{
    public class _LoggedUserInfo : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _LoggedUserInfo(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.namesurname = user.Name + " " + user.Surname;
            ViewBag.image = user.Image;
            ViewBag.id = user.Id;
            return View();
        }
    }
}
