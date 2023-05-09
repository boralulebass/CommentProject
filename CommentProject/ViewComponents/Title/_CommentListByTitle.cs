using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.ViewComponents.Title
{

    public class _CommentListByTitle : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _CommentListByTitle(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke(int id)
        {
            //ViewBag.titleID= id;
            var values = _commentService.TGetCommentsByTitle(id);
            return View(values);
        }
    }
}
