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
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetCommentsByTitle(int id)
        {
           var context = new Context();
            return context.Comments.Include(x=>x.Title).Include(x=>x.AppUser).Include(x=>x.Title.Category).Where(x=>x.TitleID == id).ToList();
        }

        public List<Comment> GetCommentsByUser(int id)
        {
            var context = new Context();
            return context.Comments.Include(x => x.Title).Include(x => x.AppUser).Include(x => x.Title.Category).Where(x => x.AppUserID == id).ToList();
        }

        public List<Comment> GetCommentsIncluded0()
        {
            var context = new Context();
            return context.Comments.Include(x => x.Title).Include(x => x.AppUser).Include(x => x.Title.Category).ToList();
        }
    }
}
