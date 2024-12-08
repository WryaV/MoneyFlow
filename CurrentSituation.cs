using System;
using System.Linq;
using Domain;
using dataHandeling;

namespace MoneyFlow
{
    public class CurrentSituation
    {
        private readonly DataHandeling _context;
        private readonly User _currentUser;

        public CurrentSituation(DataHandeling context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public void Display()
        {
            // Retrieve total balance for the current user
            var total = _context.Totals.FirstOrDefault(t => t.Id == _currentUser.Id);

            // Retrieve the latest 5 transactions for the current user
            var flows = _context.Flows
                .Where(f => f.UserId == _currentUser.Id)
                .OrderByDescending(f => f.dateTime)
                .Take(5)
                .ToList();

            Console.WriteLine($"\nCurrent Situation for {_currentUser.FirstName} {_currentUser.LastName}:");
            Console.WriteLine($"Total Balance: {ColorizeAmount(total?.TotalFlow ?? 0)}");

            Console.WriteLine("\nRecent Transactions:");
            foreach (var flow in flows)
            {
                string type = flow.Income > 0 ? "Income" : "Expense";
                string formattedAmount = flow.Income > 0 ? ColorizeAmount(flow.Income) : ColorizeAmount(flow.Expense);
                Console.WriteLine($"{flow.dateTime.ToShortDateString()} - {type}: {formattedAmount}");
            }
        }

        // Add color coding to the displayed amount
        private string ColorizeAmount(decimal amount)
        {
            return amount > 0 ? $"\x1b[32m${amount}\x1b[0m" : $"\x1b[31m${amount}\x1b[0m";
        }
    }
}
