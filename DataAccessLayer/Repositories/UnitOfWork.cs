using DataAccessLayer.DB;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        internal WalletContext _context;
        internal IRepository<Account> _accountRepo;
        internal IRepository<Transaction> _transactionRepo;
        internal IRepository<Category> _categoryRepo;

        public UnitOfWork(WalletContext context, IRepository<Account> accountRepo,
            IRepository<Transaction> transactionRepo, IRepository<Category> categoryRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _categoryRepo = categoryRepo;
        }

        public IRepository<Account> AccountRepo
        {
            get
            {
                return _accountRepo;
            }
        }

        public IRepository<Transaction> TransactionRepo
        {
            get
            {
                return _transactionRepo;
            }
        }

        public IRepository<Category> CategoryRepo
        {
            get
            {
                return _categoryRepo;
            }
        }




        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
