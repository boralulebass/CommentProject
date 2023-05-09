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
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public void TDelete(Contact t)
        {
           _contactDal.Delete(t);
        }

        public Contact TGetByID(int id)
        {
           return _contactDal.GetByID(id);
        }

        public List<Contact> TGetList()
        {
           return _contactDal.GetList();
        }

        public List<Contact> TGetListByFilter(Expression<Func<Contact, bool>> filter)
        {
           return _contactDal.GetListByFilter(filter);
        }

        public List<Contact> TGetListIncluded()
        {
            return _contactDal.GetListIncluded().OrderByDescending(x=>x.MessageId).ToList();
        }

        public void TInsert(Contact t)
        {
            _contactDal.Insert(t);
        }

        public void TUpdate(Contact t)
        {
           _contactDal.Update(t);
        }
    }
}
