using CommentProject.BusinessLayer.Abstract;
using CommentProject.DataAccessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<Comment> GetTCommentsByUser(int id)
        {
          return   _commentDal.GetCommentsByUser(id).Where(x => x.CommentStatus == true).ToList();
        }

        public void TDelete(Comment t)
        {
            _commentDal.Delete(t);
        }

        public Comment TGetByID(int id)
        {
            return _commentDal.GetByID(id);
        }

        public List<Comment> TGetCommentsByTitle(int id)
        {
            return _commentDal.GetCommentsByTitle(id).Where(x => x.CommentStatus == true).ToList();
        }

        public List<Comment> TGetCommentsIncluded0()
        {
            return _commentDal.GetCommentsIncluded0().Where(x => x.CommentStatus == true).ToList();
        }

        public List<Comment> TGetList()
        {
            return _commentDal.GetList().Where(x => x.CommentStatus == true).ToList(); ;
        }

        public List<Comment> TGetListByFilter(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.GetListByFilter(filter);
        }

        public void TInsert(Comment t)
        {
            t.CommentStatus = true;
            _commentDal.Insert(t);
        }

        public void TUpdate(Comment t)
        {
            _commentDal.Update(t);
        }
    }
}
