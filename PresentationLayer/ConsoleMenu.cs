using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;

namespace PresentationLayer
{
    public class ConsoleMenu : IConsoleMenu
    {
        private IService _service;
        public ConsoleMenu(IService service) {
        }
        public void Start()
        {
            while (true) {
                Console.WriteLine("Wallet application");
                Console.WriteLine("Enter 1 to see available accounts");
                Console.WriteLine("Enter 2 to create new account");
                int input;
                bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out input);
                if (parsedSuccessfully)
                {
                    switch (input)
                    {
                        case 1:
                            DisplayAccounts();
                            break;
                        case 2:
                            AccountCreate();
                            break;
                        default:
                            Console.WriteLine("Unknown command, try again");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, try again");
                }
            }
        }

        private void DisplayAccounts()
        {
            while (true)
            {

                Console.WriteLine("Available accounts: ");
                List<Account> accounts = _service.GetAllAccounts().ToList();
                int counter = 1;
                foreach (Account account in accounts)
                {
                    Console.WriteLine("Account " + counter + ". " + account.Name);
                    Console.WriteLine("Current balance: " + account.CurrentBalance);
                    Console.WriteLine("");
                    counter++;
                }

                Console.WriteLine("Enter account number you want to choose: ");
                int input;
                bool parsedSuccesfully = int.TryParse(Console.ReadLine(), out input);
                if (parsedSuccesfully)
                {
                    Account current = accounts[input - 1];
                    bool looping = true;
                    while (looping)
                    {
                        Console.WriteLine(current.Name);
                        Console.WriteLine("Current balance: " + current.CurrentBalance);
                        Console.WriteLine("Enter 1 to see all transactions");
                        Console.WriteLine("2 to see incomes and expenses by category");
                        Console.WriteLine("3 to add income");
                        Console.WriteLine("4 to add expense");
                        Console.WriteLine("5 to transfer money between your accounts");
                        Console.WriteLine("9 to delete this account");
                        Console.WriteLine("0 to go back");
                        int action;
                        bool success = int.TryParse(Console.ReadLine(), out action);
                        if (success)
                        {
                            switch (action)
                            {
                                case 1:
                                    DisplayTransactions(current);
                                    break;
                                case 2:
                                    DisplayCategories(current); 
                                    break;
                                case 3:
                                    AddIncome(current);
                                    break;
                                case 4:
                                    AddExpense(current);
                                    break;
                                case 5:
                                    TransferBetweenAccounts(current);
                                    break;
                                case 9: 
                                    DeleteAccount(current);
                                    break;
                                case 0:
                                    looping = false;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, try again");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, try again");
                }
                
            }
        }

        private void AccountCreate()
        {
            Console.WriteLine("Creating account");
            Console.WriteLine("Enter account name: ");
            string input = Console.ReadLine();
            Account account = new Account
            {
                CurrentBalance = 0,
                Name = input
            };
            _service.AddAccount(account);
        }

        private void DisplayTransactions(Account current)
        {
            bool looping = true;
            while (looping)
            {
                Console.WriteLine("Enter 1 to see incoming transactions");
                Console.WriteLine("Enter 2 to see outcoming transactions");

            }
        }

        private void DisplayCategories(Account current)
        {
            //todo
        }

        private void AddIncome(Account current)
        {
            //todo
        }

        private void AddExpense(Account current)
        {
            //todo
        }

        private void TransferBetweenAccounts(Account current)
        {
            //todo
        }

        private void DeleteAccount(Account current)
        {
            //todo
        }
    }
}