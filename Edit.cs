using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MoneyFlow
{
    public class Edit
    {
        private string filePath;

        public Edit(string filePath)
        {
            this.filePath = filePath;
        }

        // Display all transactions
        public void DisplayTransactions()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {lines[i]}");
                }
            }
            else
            {
                Console.WriteLine("No transactions available.");
            }
        }

        // Edit a selected transaction
        public void EditTransaction(int transactionNumber)
        {
            var lines = File.ReadAllLines(filePath).ToList();

            if (transactionNumber > 0 && transactionNumber <= lines.Count)
            {
                Console.WriteLine($"Current transaction: {lines[transactionNumber - 1]}");

                // Initialize new values
                string newDate = null;
                decimal newIncome = 0;
                decimal newExpense = 0;
                decimal newTax = 0;

                // New date
                Console.WriteLine("Enter the new date (format: YYYY-MM-DD) or type 'exit' to skip:");
                newDate = Console.ReadLine();
                if (newDate.ToLower() == "exit") newDate = null;

                // New income
                Console.WriteLine("Enter the new income or type 'exit' to skip:");
                string incomeInput = Console.ReadLine();
                if (incomeInput.ToLower() != "exit" && decimal.TryParse(incomeInput, out newIncome) == false)
                {
                    newIncome = 0; // if invalid input, default to 0
                }

                // New expense
                Console.WriteLine("Enter the new expense or type 'exit' to skip:");
                string expenseInput = Console.ReadLine();
                if (expenseInput.ToLower() == "exit" && decimal.TryParse(expenseInput, out newExpense) == false)
                {
                    newExpense = 0; // if invalid input, default to 0
                }

                // New tax
                Console.WriteLine("Enter the new tax or type 'exit' to skip:");
                string taxInput = Console.ReadLine();
                if (taxInput.ToLower() == "exit" && decimal.TryParse(taxInput, out newTax) == false)
                {
                    newTax = 0; // if invalid input, default to 0
                }

                // Update the selected transaction
                string updatedTransaction = $"{(newDate ?? lines[transactionNumber - 1].Split(',')[0])},{newIncome},{newExpense},{newTax}";
                lines[transactionNumber - 1] = updatedTransaction;

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Transaction updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid transaction number.");
            }
        }

        // Delete a selected transaction
        public void DeleteTransaction(int transactionNumber)
        {
            var lines = File.ReadAllLines(filePath).ToList();

            if (transactionNumber > 0 && transactionNumber <= lines.Count)
            {
                // Remove the selected transaction
                lines.RemoveAt(transactionNumber - 1);

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Transaction deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid transaction number.");
            }
        }
    }
}
