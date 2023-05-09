using CommentProject.BusinessLayer.Abstract;
using CommentProject.BusinessLayer.ValidationRules;
using CommentProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using X.PagedList;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CommentProject.Controllers
{
    public class WriterTitleController : Controller
    {
        private readonly ITitleService _titleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public WriterTitleController(ITitleService titleService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _titleService = titleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1/*, string keyword*/)
        {
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    var valuesSearched = _titleService.TGetList().OrderByDescending(x => x.TitleID).ToList().Where(x=>x.TitleName.Contains(keyword)).ToPagedList(page, 8);
            //    return View(valuesSearched);
            //}

            var values = _titleService.TGetList().OrderByDescending(x => x.TitleID).ToList().ToPagedList(page, 8);
            return View(values);
        }
        [Authorize(Roles = "Yazar")]
        [HttpGet]
        public IActionResult AddTitle(int id)
        {

            List<SelectListItem> values = (from x in _categoryService.TGetList().Where(x => x.CategoryStatus == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }

        [HttpPost]
        public IActionResult AddTitle(Title title)
        {
            TitleAddValidator wv = new TitleAddValidator();
            FluentValidation.Results.ValidationResult validationResult = new ValidationResult();
            validationResult = wv.Validate(title);
            if (validationResult.IsValid)
            {
                title.AppUserID = (int)HttpContext.Session.GetInt32("LoggerID");
                title.TitleStatus = true;
                _titleService.TInsert(title);
                return RedirectToAction("MyTitles", "WriterTitle");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }
        [Authorize(Roles = "Yazar")]
        public IActionResult MyTitles(int page = 1)
        {
            var values = _titleService.TTitleIncluded().Where(x => x.AppUserID == (int)HttpContext.Session.GetInt32("LoggerID")).OrderByDescending(x => x.TitleID).ToList().ToPagedList(page, 5);
            if (values.Count == 0)
            {
                ViewBag.message = "Hiç başlığınız yok, hemen şimdi oluşturun!";
            }
            return View(values);
        }
        public IActionResult TitlesByCategory(int id, int page = 1)
        {
            var categoryname = _categoryService.TGetByID(id);
            ViewBag.categoryname = categoryname.CategoryName;
            var values = _titleService.TTitleIncluded().Where(x => x.CategoryID == id).OrderByDescending(x => x.TitleID).ToList().ToPagedList(page, 5);
            if (values.Count() == 0)
            {
                if (User.IsInRole("Yazar"))
                {
                    ViewBag.info = "Bu kategoride hiç başlık bulunmamaktadır. İlkini sen oluştur!";
                }
                else
                {
                    ViewBag.info = "Bu kategoride hiç başlık bulunmamaktadır. Üye ol ve ilkini sen oluştur!";
                }
            }
            return View(values);
        }
        [Authorize(Roles = "Yazar")]
        [HttpGet]
        public IActionResult AddTitleWCategory(int id)
        {
            List<SelectListItem> values = (from x in _categoryService.TGetList().Where(x => x.CategoryID == id).Where(x => x.CategoryStatus == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()

                                           }).ToList();
            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddTitleWCategory(Title title)
        {

            TitleAddValidator wv = new TitleAddValidator();
            FluentValidation.Results.ValidationResult validationResult = new FluentValidation.Results.ValidationResult();
            validationResult = wv.Validate(title);
            if (validationResult.IsValid)
            {
                title.AppUserID = (int)HttpContext.Session.GetInt32("LoggerID");
                title.TitleStatus = true;
                _titleService.TInsert(title);
                return RedirectToAction("MyTitles", "WriterTitle");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }
    }
}
