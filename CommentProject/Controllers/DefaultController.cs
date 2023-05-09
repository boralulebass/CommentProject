using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        private readonly ITitleService _titleService;

        public DefaultController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public IActionResult Index(int id)
        {
            ViewBag.i = id;       
            return View();
        }
    }
}
