﻿using DataAccessLayer.Entities;
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

            sender.CurrentBalance -= amount;
            _transactionService.CreateTransaction(transaction);
            _accountService.UpdateAccount(sender);

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

            recipient.CurrentBalance += amount;
            _transactionService.CreateTransaction(transaction);
            _accountService.UpdateAccount(recipient);
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

            sender.CurrentBalance -= amount;
            recipient.CurrentBalance += amount;
           _transactionService.CreateTransaction(transaction);
            _accountService.UpdateAccount(sender);
            _accountService.UpdateAccount(recipient);
        }

        public void AddAccount(Account account)
        {
            _accountService.AddAccount(account);
        }

        public IEnumerable<Transaction> GetIncomes(Account recipient)
        {
            return _transactionService.GetIncomesByAccount(recipient);
        }

        public IEnumerable<Transaction> GetExpenses(Account sender)
        {
            return _transactionService.GetExpensesByAccount(sender);
        }

        public Dictionary<string, decimal> GetCategories(Account account)
        {
            IEnumerable<Transaction> Transactions = _transactionService.GetAllTransactionsByAccount(account);
            Dictionary<string, decimal> categories = new Dictionary<string, decimal>();
            foreach(Transaction transaction in Transactions)
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

            return categories;
        }

        public IEnumerable<Account> GetOtherAccounts(Account account)
        {
            return _accountService.getOtherAccounts(account);
        }
    }
}
