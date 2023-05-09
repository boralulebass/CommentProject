using CommentProject.BusinessLayer.Abstract;
using CommentProject.BusinessLayer.Concrete;
using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public LikeController(ILikeService likeService, ICommentService commentService, UserManager<AppUser> userManager)
        {
            _likeService = likeService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddLike(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ifLiked = _likeService.TGetList().Where(x=>x.AppUserID == user.Id).Where(x=>x.CommentID == id).FirstOrDefault();
            var likeObj = new Like()
            {
                AppUserID = user.Id,
                CommentID = id,
            };
            if (ifLiked == null) 
            {
                _likeService.TInsert(likeObj);
                return View();
            }
            else
            {
                _likeService.TDelete(ifLiked);
            }
            return RedirectToAction("Index","Category");
        }
    }
}
