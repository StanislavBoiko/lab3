using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class Service : IService
    {
        internal IAccountService _accountService;
        internal ITransactionService _transactionService;

        public Service (IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public void AddExpense(Account sender, string Category, decimal amount)
        {
            Transaction transaction = new Transaction
            { 
                Id = Guid.NewGuid(),
                Amount = amount, 
                Category = Category, 
                Sender = sender,
                SenderId = sender.Id,
            };
            
            _transactionService.CreateTransaction(transaction);

        }

        public void AddIncome(Account recipient, string Category, decimal amount)
        {
            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Category = Category,
                Recipient = recipient,
                RecipientId = recipient.Id,
            };
            
            _transactionService.CreateTransaction(transaction);
        }

        public Account GetAccountById(int id)
        {
            return _accountService.GetAccountById(id);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountService.GetAllAccounts();
        }

        public void TransferBetweenAccounts(Account sender, Account recipient, decimal amount)
        {
            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Sender = sender,
                SenderId = sender.Id,
                Recipient = recipient,
                RecipientId = recipient.Id,
                Category = "Transfer from " + sender.Name + " to " + recipient.Name
            };
            
           _transactionService.CreateTransaction(transaction);
        }

        public void AddAccount(Account account)
        {
            _accountService.AddAccount(account);
        }

        public IEnumerable<Transaction> GetIncomes(Account recipient)
        {
            return recipient.Incoming;
        }

        public IEnumerable<Transaction> GetExpenses(Account sender)
        {
            return sender.Outgoing;
        }

        public Dictionary<string, decimal> GetCategories(Account account)
        {
            IEnumerable<Transaction> Incomes = account.Incoming;
            Dictionary<string, decimal> categories = new Dictionary<string, decimal>();
            foreach(Transaction transaction in Incomes)
            {
                string category = transaction.Category;
                
                if (categories.ContainsKey(category))
                {
                    categories[category] += transaction.Amount;
                }
                else
                {
                    categories.Add(category, transaction.Amount);
                }
            }

            IEnumerable<Transaction> Expenses = account.Outgoing;
            foreach (Transaction transaction in Expenses)
            {
                string category = transaction.Category;
                
                if (categories.ContainsKey(category))
                {
                    categories[category] -= transaction.Amount;
                }
                else
                {
                    categories.Add(category, - transaction.Amount);
                }
            }

            return categories;
        }

        public IEnumerable<Account> GetOtherAccounts(Account account)
        {
            return _accountService.getOtherAccounts(account);
        }
    }
}
