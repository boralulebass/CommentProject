using CommentProject.BusinessLayer.Abstract;
using CommentProject.DataAccessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.BusinessLayer.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal _likeDal;

        public LikeManager(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public void TDelete(Like t)
        {
            _likeDal.Delete(t);
        }

        public Like TGetByID(int id)
        {
           return _likeDal.GetByID(id);
        }

        public List<Like> TGetList()
        {
            return _likeDal.GetList();
        }

        public List<Like> TGetListByFilter(Expression<Func<Like, bool>> filter)
        {
           return _likeDal.GetListByFilter(filter);
        }

        public void TInsert(Like t)
        {
           _likeDal.Insert(t);
        }

        public void TUpdate(Like t)
        {
            _likeDal.Update(t);
        }
    }
}
