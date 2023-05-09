using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {
        public IActionResult Page404()
        {
            return View();
        }
        public IActionResult Page403()
        {

            return View();
        }
    }
}
