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
        Account GetAccountById(int id);
        void AddIncome(Account recipient, Category Category, decimal amount);
        void AddExpense(Account sender, Category Category, decimal amount);
        void TransferBetweenAccounts(Account sender, Account recipient, decimal amount);
        void AddAccount(Account account);
        IEnumerable<Transaction> GetIncomes(Account recipient);
        IEnumerable<Transaction> GetExpenses(Account sender);

        Dictionary<Category, decimal> GetCategories(Account account);
        IEnumerable<Account> GetOtherAccounts(Account account);
    }
}
