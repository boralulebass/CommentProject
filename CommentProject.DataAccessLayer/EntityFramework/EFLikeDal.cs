using CommentProject.DataAccessLayer.Abstract;
using CommentProject.DataAccessLayer.Repositories;
using CommentProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.DataAccessLayer.EntityFramework
{
    public class EFLikeDal : GenericRepository<Like>, ILikeDal
    {
    }
}
