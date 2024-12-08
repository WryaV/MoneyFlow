using System;
using Domain;
using dataHandeling;

namespace MoneyFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DataHandeling()) // Initialize database context
            {
                context.Database.EnsureCreated(); // Ensure the database is created

                PasswordManager passwordManager = new PasswordManager(context);
                User currentUser = null;

                Console.WriteLine("1- Register\n2- Login");
                int choice = int.Parse(Console.ReadLine()); // Get user input for login or registration

                if (choice == 1)
                {
                    currentUser = passwordManager.RegisterUser(); // Register a new user
                }
                else if (choice == 2)
                {
                    currentUser = passwordManager.AuthenticateUser(); // Authenticate an existing user
                }

                if (currentUser == null) // Deny access if user authentication or registration fails
                {
                    Console.WriteLine("Access denied.");
                    return;
                }

                Console.WriteLine("Welcome to the MoneyFlow app!");

                while (true)
                {
                    Console.WriteLine("\nMain Menu:");
                    Console.WriteLine("-----------------");
                    Console.WriteLine("1- New Transaction");
                    Console.WriteLine("2- Current Situation");
                    Console.WriteLine("3- Exit");
                    Console.WriteLine("-----------------");

                    Console.Write("Choose an option: ");
                    string mainInput = Console.ReadLine();

                    if (mainInput == "3") // Exit the application
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }

                    switch (mainInput)
                    {
                        case "1":
                            new AddExorIn(context, currentUser); // Handle new transactions
                            break;

                        case "2":
                            var currentSituation = new CurrentSituation(context, currentUser); // Display current financial situation
                            currentSituation.Display();
                            break;

                        default:
                            Console.WriteLine("Invalid choice, please try again."); // Handle invalid menu options
                            break;
                    }
                }
            }
        }
    }
}
