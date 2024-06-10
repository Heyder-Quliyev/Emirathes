using emirathes.IRepository;
using emirathes.Models;
using emirathes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace emirathes.Repository
{
    public class ContactUserRepository : Repository<ContactUser>, IContactUserRepository
    {
        private readonly AppDbContent _db;
        public ContactUserRepository(AppDbContent db) : base(db)
        {
            _db = db;
        }

        public void Update(ContactUser obj)
        {
            _db.ContactUsers.Update(obj);
        }
    }
}