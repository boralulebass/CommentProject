using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;

namespace CommentProject.ViewComponents.Comment
{
    public class _CommentListByUser: ViewComponent
    {
        private readonly ICommentService _commentService;

        public _CommentListByUser(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int id)
        {
           var values = _commentService.TGetCommentsIncluded0().Where(x=>x.AppUserID == id).OrderByDescending(x=>x.CommentID).ToList();
            return View(values);
        }
    }
}
