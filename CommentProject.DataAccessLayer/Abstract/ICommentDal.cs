using CommentProject.BusinessLayer.Abstract;
using CommentProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.DataAccessLayer.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        List<Comment> GetCommentsByTitle(int id);
        List<Comment> GetCommentsByUser(int id);
        List<Comment> GetCommentsIncluded0();
    }
}
