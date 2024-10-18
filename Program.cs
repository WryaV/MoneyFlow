using System;
using MoneyFlow;

class Program
{
    static void Main(string[] args)
    {
        PasswordManager passwordManager = new PasswordManager();

        // Looking for the Password file, if it does not exit, we ask the user to create one!
        if (!passwordManager.PasswordFileExists())
        {
            passwordManager.SetNewPassword();
        }
        else
        {
            // Checking the given password
            if (!passwordManager.ValidatePassword())
            {
                Console.WriteLine("Access denied.");
                return;
            }
            else
            {
                Console.WriteLine("Access granted. Welcome to the app!");
            }
        }

        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1- The Current Financial Situation");
        Console.WriteLine("2- Add new \"Expense\" or \"Income\"");
        Console.WriteLine("3- Edit the Current list");

        int userRequest = 0;
        bool userInput = false;
        while (!userInput)
        {
            string check = Console.ReadLine();
            if (int.TryParse(check, out userRequest))
            {
                if (userRequest >= 1 && userRequest <= 3)
                    userInput = true;
                else
                {
                    Console.WriteLine("The number should be 1, 2 or 3 only.");
                }
            }
            else
            {
                Console.WriteLine("Please provide an integer number.");
            }
        }

        // Declarung the filePath 
        string filePath = "financial_data.csv";
        //Menu, correspond with a class
        switch (userRequest)
        {
            case 1:
                CurrentSituation currentSituation = new CurrentSituation(filePath);
                
                // Calculate total flow
                FinancialFlowCalculator flowCalculator = new FinancialFlowCalculator(filePath);
                decimal totalFlow = flowCalculator.GetTotalFlow();
                Console.WriteLine($"Total Financial Flow: {totalFlow:C}");
                break;

            case 2:
                AddExorIn addExorIn = new AddExorIn(filePath);
                
                // After adding new income or expense, calculate total flow
                FinancialFlowCalculator calculator = new FinancialFlowCalculator(filePath);
                decimal newTotalFlow = calculator.GetTotalFlow();
                Console.WriteLine($"Updated Total Financial Flow: {newTotalFlow:C}");
                break;

            case 3:
                // Use the Edit class to edit the current financial list
                Console.WriteLine("Editing the current list.");
                Edit editTransactions = new Edit(filePath);
                
                // Display current transactions
                editTransactions.DisplayTransactions();
                
                // Ask the user to choose the transaction they want to edit or delete
                Console.WriteLine("Enter the number of the transaction you want to edit or delete:");
                if (int.TryParse(Console.ReadLine(), out int transactionNumber))
                {
                    Console.WriteLine("Would you like to (1) Edit or (2) Delete the transaction?");
                    string choice = Console.ReadLine();
                    
                    if (choice == "1")
                    {
                        editTransactions.EditTransaction(transactionNumber);
                    }
                    else if (choice == "2")
                    {
                        editTransactions.DeleteTransaction(transactionNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose 1 or 2.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
                break;

                default:
                Console.WriteLine("Invalid option.");
                break;
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
