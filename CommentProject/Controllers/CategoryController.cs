
using CommentProject.BusinessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace CommentProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _categoryService.TGetList().ToPagedList(page,6);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.TInsert(category);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCategory(int id) 
        {
            var values = _categoryService.TGetByID(id);
            _categoryService.TDelete(values);
            return RedirectToAction("Index","Category");
        }
        public IActionResult UpdateCategory(int id)
        {
            var values = _categoryService.TGetByID(id);
            if (values.CategoryStatus == true)
            {
                values.CategoryStatus = false;
                _categoryService.TUpdate(values);
                return RedirectToAction("Index", "Category");
            }
            else
            {
                values.CategoryStatus = true;
                _categoryService.TUpdate(values);
                return RedirectToAction("Index", "Category");
            }
        }
    }
}
