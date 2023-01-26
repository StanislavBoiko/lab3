using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> AccountRepo { get; }
        IRepository<Transaction> TransactionRepo { get; }
        IRepository<Category> CategoryRepo { get; }

        void Save();
    }
}
