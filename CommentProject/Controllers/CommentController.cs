using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using X.PagedList;

namespace CommentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _commentService.TGetCommentsIncluded0().OrderByDescending(x=>x.CommentID).ToList().ToPagedList(page,6);
            return View(values);
        }
        public IActionResult DeleteComment(int id)
        {
            var values = _commentService.TGetByID(id);
            _commentService.TDelete(values);
            return RedirectToAction("Index","Comment");
        }
        public IActionResult CommentsByTitle(int id,int page = 1)
        {
            var values = _commentService.TGetCommentsIncluded0().OrderByDescending(x => x.CommentID).Where(x=>x.TitleID==id).ToList().ToPagedList(page,6);
            return View(values);
        }
    }
}
