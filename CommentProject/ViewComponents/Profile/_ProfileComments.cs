using CommentProject.BusinessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using X.PagedList;

namespace CommentProject.ViewComponents.Profile
{
    public class _ProfileComments :ViewComponent
    {
        private readonly ITitleService _titleService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;

        public _ProfileComments(ITitleService titleService, UserManager<AppUser> userManager, ICommentService commentService)
        {
            _titleService = titleService;
            _userManager = userManager;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int page = 1)
        {
            if (id == null) 
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var values = _commentService.TGetCommentsIncluded0().Where(x => x.AppUserID == user.Id).ToList().ToPagedList(page, 4);
                if (values.Count == 0)
                {
                    ViewBag.message = " Hiç yorumunuz yok, hemen şimdi yazmaya başlayın!";
                }
                return View(values);
            }
            else
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
                var values = _commentService.TGetCommentsIncluded0().Where(x => x.AppUserID == user.Id).ToList().ToPagedList(page,4);
                if (values.Count == 0)
                {
                    ViewBag.message = " Hiç yorumunuz yok, hemen şimdi yazmaya başlayın!";
                }
                return View(values);

            }
        }
    }
}
