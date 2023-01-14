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

        public IRepository<Account> AccountRepo
        {
            get
            {
                if(_accountRepo == null)
                {
                    _accountRepo = new GenericRepository<Account>(_context);
                }
                return _accountRepo;

            }
        }

       

        public IRepository<Transaction> TransactionRepo
        {
            get
            {
                if(_transactionRepo== null)
                {
                    _transactionRepo= new GenericRepository<Transaction>(_context); 

                }
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
