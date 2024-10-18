using System;
using System.Globalization;
using System.IO;

namespace MoneyFlow
{
    public class AddExorIn
    {
        private string filePath;

        public AddExorIn(string filePath)
        {
            this.filePath = filePath;
            Console.WriteLine("Which one do you want to do? ");
            Adding();
        }

        public void Adding()
        {
            while (true)
            {
                Console.WriteLine("Choose an option or type 'exit' to quit:");
                Console.WriteLine("1- Add an Expense");
                Console.WriteLine("2- Add an Income");
                string input = Console.ReadLine();

                // Check for exit command
                if (input.ToLower() == "exit")
                {
                    break; // Exit the loop
                }

                if (int.TryParse(input, out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            if (AddExpense())
                                return; // Exit if user chose to exit in AddExpense
                            break;
                        case 2:
                            if (AddIncome())
                                return; // Exit if user chose to exit in AddIncome
                            break;
                        default:
                            Console.WriteLine("Please choose 1 to add an expense or 2 to add an income.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number (1 for Expense, 2 for Income) or type 'exit' to quit.");
                }
            }
        }

        private bool AddExpense()
        {
            Console.WriteLine("Please provide the following information:");

            // Date
            DateTime date = GetDateInput();
            if (date == DateTime.MinValue) return true; // Exit condition

            // Expense amount
            decimal expenseAmount = GetAmountInput("Expense");
            if (expenseAmount < 0) return true; // Exit condition

            // Append data to the CSV file
            AppendToCsv(date, 0, expenseAmount, 0);
            return false; // Not exited
        }

        private bool AddIncome()
        {
            Console.WriteLine("Please provide the following information:");

            // Date
            DateTime date = GetDateInput();
            if (date == DateTime.MinValue) return true; // Exit condition

            // Income amount
            decimal incomeAmount = GetAmountInput("Income");
            if (incomeAmount < 0) return true; // Exit condition

            // Tax calculation (e.g., 10% tax)
            decimal tax = CalculateTax(incomeAmount);

            // Append data to the CSV file
            AppendToCsv(date, incomeAmount, 0, tax);
            return false; // Not exited
        }

        private DateTime GetDateInput()
        {
            DateTime date = DateTime.MinValue;
            bool validDate = false;

            while (!validDate)
            {
                Console.Write("Enter the date (yyyy-MM-dd) or type 'exit' to cancel: ");
                string dateString = Console.ReadLine();

                // Allow exit during date input
                if (dateString.ToLower() == "exit")
                {
                    return DateTime.MinValue; // Return a placeholder value
                }

                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                }
            }

            return date;
        }

        private decimal GetAmountInput(string type)
        {
            decimal amount = 0;
            bool validAmount = false;

            while (!validAmount)
            {
                Console.Write($"Enter the {type} amount or type 'exit' to cancel: ");
                string amountString = Console.ReadLine();

                // Allow exit during amount input
                if (amountString.ToLower() == "exit")
                {
                    return -1; // Return a negative value to indicate exit
                }

                if (decimal.TryParse(amountString, out amount))
                {
                    validAmount = true;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a numeric value.");
                }
            }

            return amount;
        }

        private decimal CalculateTax(decimal incomeAmount)
        {
            // Tax rate of 10% (example)
            decimal taxRate = 0.10m;
            return incomeAmount * taxRate;
        }

        private void AppendToCsv(DateTime date, decimal income, decimal expense, decimal tax)
        {
            // Check if the date is valid
            if (date != DateTime.MinValue) 
            {
                string entry = $"{date.ToString("yyyy-MM-dd")},{income},{expense},{tax}";
                File.AppendAllText(filePath, entry + Environment.NewLine);
            }
        }
    }
}
