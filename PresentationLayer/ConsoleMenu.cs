using System;
using System.Collections;
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
        public ConsoleMenu(IService service)
        {
            _service = service;
        }
        public ConsoleMenu(){}
        
        public void Start()
        {
            Console.WriteLine("trigger");
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
                Console.WriteLine("2 to see outcoming transactions");
                Console.WriteLine("0 to go back");
                int input;
                bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out input);
                if (parsedSuccessfully)
                {
                    switch(input)
                    {
                        case 1:
                            GetIncomes(current);
                            break;
                        case 2:
                            GetExpenses(current);
                            break;
                        case 0:
                            looping= false;
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

        private void DisplayCategories(Account current)
        {
            Dictionary<string, decimal> categories = _service.GetCategories(current);
            foreach (KeyValuePair<string, decimal> entry in categories)
            {
                Console.WriteLine(entry.Key);
                if (entry.Value > 0)
                {
                    Console.WriteLine("Received in total: " + entry.Value);
                }
                else
                {
                    Console.WriteLine("Spent in total: " + (- entry.Value));
                }
            }
        }

        private void AddIncome(Account current)
        {
            Console.WriteLine("Adding income");
            Console.WriteLine("Enter the category of income");
            string input = Console.ReadLine();
            Console.WriteLine("Enter amount");
            decimal amount;
            decimal.TryParse(Console.ReadLine(), out amount);
            _service.AddIncome(current, input, amount);
        }

        private void AddExpense(Account current)
        {
            Console.WriteLine("Adding expense");
            Console.WriteLine("Enter the category of expense");
            string input = Console.ReadLine();
            Console.WriteLine("Enter amount");
            decimal amount;
            decimal.TryParse(Console.ReadLine(), out amount);
            _service.AddExpense(current, input, amount);
        }

        private void TransferBetweenAccounts(Account current)
        {
            Account recipient = default;
            Console.WriteLine("Transferring to another account");
            Console.WriteLine("Select an account you want to transfer money to");
            List<Account> accounts = _service.GetOtherAccounts(current).ToList();
            int counter = 1;
            foreach (Account account in accounts)
            {
                Console.WriteLine(counter + ". Acccount " + account.Name);
                Console.WriteLine("Current balance: " + account.CurrentBalance);
                counter++;
            }

            bool looping = true;
            while (true)
            {
                Console.WriteLine("Enter the corresponding account number");
                Console.WriteLine("0 to go back");
                int input;
                bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out input);
                if (parsedSuccessfully)
                {
                    if (input == 0)
                    {
                        break;
                    }
                    recipient = accounts[input - 1];
                }
                else
                {
                    Console.WriteLine("Invalid input, try again");
                }

                while (true)
                {
                    Console.WriteLine("Enter the amount of money you want to transfer");
                    decimal amount;
                    bool success = decimal.TryParse(Console.ReadLine(), out amount);
                    if (success)
                    {
                        if (amount <= 0)
                        {
                            Console.WriteLine("Amount of transferred money should be greater than zero");
                        }
                        _service.TransferBetweenAccounts(current, recipient, amount);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, try again");
                    }
                }
            }
        }
        private void GetExpenses(Account current)
        {
            IEnumerable<Transaction> expenses = _service.GetExpenses(current);
            foreach (Transaction t in expenses)
            {
                Console.WriteLine(t.Category);
                Console.WriteLine(t.Amount);
                Console.WriteLine(t.DateTime);
            }
        }

        private void GetIncomes(Account current)
        {
            IEnumerable<Transaction> incomes = _service.GetIncomes(current);
            foreach (Transaction t in incomes)
            {
                Console.WriteLine(t.Category);
                Console.WriteLine(t.Amount);
                Console.WriteLine(t.DateTime);
            }
        }


    }
}