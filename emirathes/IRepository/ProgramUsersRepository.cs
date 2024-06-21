using emirathes.Models;
using emirathes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace emirathes.IRepository
{
    public class ProgramUsersRepository : Repository<ProgramUsers>, IProgramUsers
    {
        private readonly AppDbContent _db;
        public ProgramUsersRepository (AppDbContent db) : base(db)
        {
            _db = db;
        }
    }
}