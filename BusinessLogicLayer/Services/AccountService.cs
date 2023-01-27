using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {

        internal IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddAccount(Account account)
        {
            _uow.AccountRepo.Create(account);
            _uow.Save();
        }

        public Account GetAccountById(int id)
        {
            return _uow.AccountRepo.GetById(id);
        }

        

        public IEnumerable<Account> GetAllAccounts()
        {
            return _uow.AccountRepo.Get();
        }
        

        public IEnumerable<Account> getOtherAccounts(Account account)
        {
            return _uow.AccountRepo.Get(a => !a.Equals(account));
        }
    }
}
