using CommentProject.BusinessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using X.PagedList;
using X.PagedList.Web.Common;

namespace CommentProject.ViewComponents.Profile
{
    public class _ProfileTitles : ViewComponent
    {
        private readonly ITitleService _titleService;
        private readonly UserManager<AppUser> _userManager;

        public _ProfileTitles(ITitleService titleService, UserManager<AppUser> userManager)
        {
            _titleService = titleService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int page = 1)
        {
            if (id == null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var values = _titleService.TGetList().Where(x => x.AppUserID == user.Id).ToList().ToPagedList(page, 4);
                if(values.Count == 0)
                {
                    ViewBag.message = "Hiç başlığınız yok, hemen şimdi oluşturun!";
                }
                return View(values);
            }
            else
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
                var values = _titleService.TGetList().Where(x => x.AppUserID == user.Id).ToList().ToPagedList(page, 4);
                if (values.Count == 0)
                {
                    ViewBag.message = "Hiç başlığınız yok, hemen şimdi oluşturun!";
                }
                return View(values);
            }

        }
    }
}
