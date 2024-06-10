using emirathes.IRepository;
using emirathes.Models;
using emirathes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emirathes.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContent _db;
       
        public IContactUserRepository ContactUser { get; private set; }
        
        public UnitOfWork(AppDbContent db)
        {
            _db = db;
           
            ContactUser = new ContactUserRepository(_db);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}