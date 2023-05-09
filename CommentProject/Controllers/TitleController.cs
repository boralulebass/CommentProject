using CommentProject.BusinessLayer.Abstract;
using CommentProject.BusinessLayer.ValidationRules;
using CommentProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Configuration;
using System.Data;
using System.Reflection.Metadata;
using X.PagedList;

namespace CommentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TitleController : Controller
    {
        private readonly ITitleService _titleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public TitleController(ITitleService titleService, UserManager<AppUser> userManager, ICategoryService categoryService)
        {
            _titleService = titleService;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _titleService.TTitleIncluded().ToPagedList(page,6);
            return View(values);
        }
        public IActionResult DeleteTitle(int id)
        {
            var values = _titleService.TGetByID(id);
            values.TitleStatus = false;
            _titleService.TUpdate(values);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddTitle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTitle(Title title)
        {
            TitleAddValidator wv = new TitleAddValidator();
            ValidationResult validationResult = new ValidationResult();
            validationResult = wv.Validate(title);
            if (validationResult.IsValid)
            {
                var titleCreatorUser = await _userManager.FindByNameAsync(User.Identity.Name);
                title.AppUserID = titleCreatorUser.Id;
                _titleService.TInsert(title);
                return RedirectToAction("Index", "Title");
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

        public IActionResult TitlesByCategory(int id, int page = 1)
        {
            var values = _titleService.TTitleIncluded().Where(x => x.CategoryID == id).OrderByDescending(x=>x.TitleID).ToList().ToPagedList(page,6);
            var category = _categoryService.TGetByID(id);
            ViewBag.categoryName = category.CategoryName;
            return View(values);
        }
    }
}

