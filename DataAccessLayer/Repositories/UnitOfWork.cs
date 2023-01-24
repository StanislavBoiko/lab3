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

        public UnitOfWork(WalletContext context, IRepository<Account> accountRepo,
            IRepository<Transaction> transactionRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
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
