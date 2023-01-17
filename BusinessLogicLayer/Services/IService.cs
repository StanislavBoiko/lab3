using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IService
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid id);
        void AddIncome(Account recipient, string Category, decimal amount);
        void AddExpense(Account sender, string Category, decimal amount);
        void TransferBetweenAccounts(Account sender, Account recipient, decimal amount);
        void AddAccount(Account account);
        IEnumerable<Transaction> GetIncomes(Account recipient);
        IEnumerable<Transaction> GetExpenses(Account sender);

        Dictionary<string, decimal> GetCategories(Account account);
        IEnumerable<Account> GetOtherAccounts(Account account);


    }
}
