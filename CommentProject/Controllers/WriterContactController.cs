using CommentProject.BusinessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.Controllers
{
    public class WriterContactController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IContactService _contactService;

        public WriterContactController(UserManager<AppUser> userManager, IContactService contactService)
        {
            _userManager = userManager;
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            _contactService.TInsert(contact);
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Contact() 
        {
           var values = _contactService.TGetList();
            return View(values);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ContactDelete(Contact contact) 
        {
            _contactService.TDelete(contact);
            return RedirectToAction("Index");
        }
    }
}
