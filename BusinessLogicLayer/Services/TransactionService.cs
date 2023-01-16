using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    internal class TransactionService : ITransactionService
    {
        internal IUnitOfWork _uow;
        public TransactionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void CreateTransaction(Transaction transaction)
        {
            _uow.TransactionRepo.Create(transaction);
            _uow.Save();
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return _uow.TransactionRepo.GetById(transactionId);
        }

        public IEnumerable<Transaction> GetIncomesByAccount(Account recipient) 
        {
            return _uow.TransactionRepo.Get(t => t.RecipientId== recipient.Id);
        }

        public IEnumerable<Transaction> GetExpensesByAccount(Account sender)
        {
            return _uow.TransactionRepo.Get(t => t.SenderId == sender.Id);
        }
    }
}
