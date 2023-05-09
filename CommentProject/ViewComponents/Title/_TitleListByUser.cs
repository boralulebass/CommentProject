using CommentProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CommentProject.ViewComponents.Title
{
    public class _TitleListByUser : ViewComponent
    {
        private readonly ITitleService _titleService;

        public _TitleListByUser(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public IViewComponentResult Invoke(int id) 
        {
            var values = _titleService.TGetListByFilter(x=>x.AppUserID == id).OrderByDescending(x=>x.TitleID).ToList();
            return View(values); 
        }
    }
}
