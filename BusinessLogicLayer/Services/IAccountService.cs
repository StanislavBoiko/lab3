using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IAccountService
    {
        void AddAccount(Account account);
        void RemoveAccount(Guid id);
        void UpdateAccount(Account account);
        Account GetAccountById(Guid id);
        IEnumerable<Account> GetAllAccounts();
    }
}
