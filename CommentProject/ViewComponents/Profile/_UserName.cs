using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.ViewComponents.Profile
{
    public class _UserName: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _UserName(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = " " + values.Name + " " + values.Surname; 
            return View();
        }
    }
}
