using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CommentProject.Controllers
{
    public class WriterCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly ITitleService _titleService;

        public WriterCategoryController(ICategoryService categoryService, ICommentService commentService, ITitleService titleService)
        {
            _categoryService = categoryService;
            _commentService = commentService;
            _titleService = titleService;
        }

        public IActionResult Index(int page = 1)
        {
           var values =  _categoryService.TGetList().Where(x=>x.CategoryStatus==true).OrderByDescending(x=>x.CategoryID).ToList().ToPagedList(page,5);
            return View(values);
        }
    }
}
