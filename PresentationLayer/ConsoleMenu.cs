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

                    }
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
                    Console.WriteLine(current.Name);
                    Console.WriteLine("Current balance: " + current.CurrentBalance);
                    Console.WriteLine("Enter 1 to see all transactions");
                    Console.WriteLine("2 to see incomes and expenses by category");
                    Console.WriteLine("3 to add income");
                    Console.WriteLine("4 to spend expense");
                    Console.WriteLine("5 to transfer money between your accounts");
                    Console.WriteLine("0 to go back");
                }
            }
        }

        private void AccountCreate()
        {
            //TODO
        }
    }
}