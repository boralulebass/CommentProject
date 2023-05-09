using CommentProject.BusinessLayer.Abstract;
using CommentProject.BusinessLayer.ValidationRules;
using CommentProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data;
using X.PagedList;

namespace CommentProject.Controllers
{
    public class WriterCommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITitleService _titleService;
        private readonly ICategoryService _categoryService;

        public WriterCommentController(ICommentService commentService, UserManager<AppUser> userManager, ITitleService titleService, ICategoryService categoryService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _titleService = titleService;
            _categoryService = categoryService;
        }
        [Authorize(Roles = "Yazar")]
        [HttpGet]
        public IActionResult Index()
        {
            List<SelectListItem> values = (from x in _titleService.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.TitleName,
                                               Value = x.TitleID.ToString()
                                           }).ToList();



            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Comment comment)
        {
            CommentValidator wv = new CommentValidator();
            ValidationResult validationResult = new ValidationResult();
            validationResult = wv.Validate(comment);
            if (validationResult.IsValid)
            {
                var titleCreatorUser = await _userManager.FindByNameAsync(User.Identity.Name);
                comment.AppUserID = titleCreatorUser.Id;
                comment.CommentStatus = true;
                comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _commentService.TInsert(comment);
                return RedirectToAction("MyComments", "WriterComment");
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
        public async Task<IActionResult> MyComments(int id, int page=1)
        {
            var commentUserID = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _commentService.GetTCommentsByUser(commentUserID.Id).OrderByDescending(x=>x.CommentID).ToList().ToPagedList(page,5);
            if(values.Count == 0)
            {
                ViewBag.message = "Hiç yorumunuz yok, hemen şimdi yazmaya başlayın!";
            }
            return View(values);
        }
        public IActionResult CommentsByTitle(int id, int page = 1)
        {
            var title =_titleService.TGetByID(id);
            ViewBag.titleID = title.TitleID;
            ViewBag.titlename = title.TitleName;
            var values = _commentService.TGetCommentsByTitle(id).OrderByDescending(x => x.CommentID).ToList().ToPagedList(page, 5);
            if (values.Count() == 0)
            {
                if (User.IsInRole("Yazar"))
                {
                    ViewBag.info = "Bu başlıkta hiç yorum bulunmamaktadır. İlkini sen oluştur!";
                }
                else
                {
                    ViewBag.info = "Bu başlıkta hiç yorum bulunmamaktadır. Üye ol ve ilkini sen oluştur!";
                }
            }
            return View(values);
        }
        public IActionResult AllComments(int page=1)
        {
            var values = _commentService.TGetCommentsIncluded0().OrderByDescending(x => x.CommentID).ToList().ToPagedList(page, 5);
            return View(values);
        }
        [Authorize(Roles = "Yazar")]
        [HttpGet]
        public IActionResult AddCommentsWTitle(int id)
        {
            List<SelectListItem> values = (from x in _titleService.TGetList().Where(x=>x.TitleID == id).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.TitleName,
                                               Value = x.TitleID.ToString()
                                           }).ToList();



            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentsWTitle(Comment comment)
        {
            CommentValidator wv = new CommentValidator();
            ValidationResult validationResult = new ValidationResult();
            validationResult = wv.Validate(comment);
            if (validationResult.IsValid)
            {
                var titleCreatorUser = await _userManager.FindByNameAsync(User.Identity.Name);
                comment.AppUserID = titleCreatorUser.Id;
                comment.CommentStatus = true;
                comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _commentService.TInsert(comment);
                return RedirectToAction("MyComments", "WriterComment");
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
