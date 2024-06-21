using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace emirathes.IRepository
{
        public interface IUnitOfWork
        {
        IProgramUsers ProgramUsers { get; }
        IContactUserRepository ContactUser { get; }
            

            void Save();
        }
    }