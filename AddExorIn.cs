using System;
using Domain;
using dataHandeling;

namespace MoneyFlow
{
    public class AddExorIn
    {
        private readonly DataHandeling _context;
        private readonly User _currentUser;

        public AddExorIn(DataHandeling context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            Adding(); // Start the process to add income or expenses
        }

        public void Adding()
        {
            while (true)
            {
                Console.WriteLine("\nChoose an option or type 'exit' to quit:");
                Console.WriteLine("1- Add an Expense");
                Console.WriteLine("2- Add an Income");
                Console.Write("Choice: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") // Exit loop on user input
                {
                    break;
                }

                if (int.TryParse(input, out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            AddTransaction(false); // Add an expense
                            break;
                        case 2:
                            AddTransaction(true); // Add an income
                            break;
                        default:
                            Console.WriteLine("Please choose 1 for expense or 2 for income.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number or type 'exit' to quit.");
                }
            }
        }

        private void AddTransaction(bool isIncome)
        {
            Console.Write("Enter the date (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the amount: ");
            int amount = int.Parse(Console.ReadLine());

            int tax = 0;
            if (isIncome)
            {
                tax = (int)(amount * 0.10m); // Calculate tax for income
            }

            var flow = new Flow
            {
                dateTime = date,
                Income = isIncome ? amount : 0,
                Expense = isIncome ? 0 : amount,
                Tax = tax,
                UserId = _currentUser.Id
            };

            _context.Flows.Add(flow); // Add the transaction record
            _context.SaveChanges();

            UpdateTotal(isIncome ? amount : -amount); // Update the total balance

            Console.WriteLine($"{(isIncome ? "Income" : "Expense")} added successfully.");
        }

        private void UpdateTotal(int amount)
        {
            var total = _context.Totals.FirstOrDefault(t => t.Id == _currentUser.Id);

            if (total == null)
            {
                total = new Total
                {
                    Id = _currentUser.Id,
                    TotalFlow = amount,
                    dateTime = DateTime.Now
                };
                _context.Totals.Add(total); // Create a new total record if none exists
            }
            else
            {
                total.TotalFlow += amount; // Update the existing total balance
                total.dateTime = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }
}
