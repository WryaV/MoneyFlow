using System;
using System.IO;

namespace MoneyFlow
{
    public class FinancialFlowCalculator
    {
        private string filePath;

        public FinancialFlowCalculator(string filePath)
        {
            this.filePath = filePath;
        }

        public decimal GetTotalFlow()
        {
            decimal totalFlow = 0;

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    // Skip header row if it exists
                    if (line.StartsWith("Date") || line.StartsWith("Income"))
                    {
                        continue;
                    }

                    var data = line.Split(',');

                    if (data.Length >= 4)
                    {
                        // Safely parse income, expense, and tax
                        if (decimal.TryParse(data[1], out decimal income) &&
                            decimal.TryParse(data[2], out decimal expense) &&
                            decimal.TryParse(data[3], out decimal tax))
                        {
                            // Calculate and add net flow
                            decimal netFlow = income - (expense + tax);
                            totalFlow += netFlow;
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid data line: {line}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return totalFlow;
        }
    }
}
