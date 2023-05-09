using CommentProject.DataAccessLayer.Abstract;
using CommentProject.DataAccessLayer.Concrete;
using CommentProject.DataAccessLayer.Repositories;
using CommentProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.DataAccessLayer.EntityFramework
{
    public class EFTitleDal : GenericRepository<Title>, ITitleDal
    {
        public List<Title> TitleIncluded()
        {
            var context = new Context();
            return context.Titles.Include(x => x.AppUser).Include(x => x.Category).Where(x=>x.TitleStatus==true).ToList();
        }
    }
}
