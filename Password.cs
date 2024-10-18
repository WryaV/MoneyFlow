using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class PasswordManager
{
    private const string PasswordFile = "password.txt";  // File to store the hashed password

    //looking for the password file
    public bool PasswordFileExists()
    {
        return File.Exists(PasswordFile);
    }

    //  using SHA256 to hash the given password
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));  
            }
            return builder.ToString();
        }
    }

    // Setting a new password
    public void SetNewPassword()
    {
        Console.WriteLine("No password set. Please create a new password:");
        string newPassword = Console.ReadLine();
        string hashedPassword = HashPassword(newPassword);
        File.WriteAllText(PasswordFile, hashedPassword);
        Console.WriteLine("Password created successfully.");
    }

    // Method to validate the password
    public bool ValidatePassword()
    {
        Console.WriteLine("Please enter your password:");
        string enteredPassword = Console.ReadLine();
        string hashedPassword = HashPassword(enteredPassword);

        string storedHashedPassword = File.ReadAllText(PasswordFile);
        return hashedPassword == storedHashedPassword;
    }

    //Method for Changing the password
    public void ChangePassword()
    {
        if (ValidatePassword())
        {
            Console.WriteLine("Enter new password:");
            string newPassword = Console.ReadLine();
            string hashedPassword = HashPassword(newPassword);
            File.WriteAllText(PasswordFile, hashedPassword);
            Console.WriteLine("Password changed successfully.");
        }
        else
        {
            Console.WriteLine("Password incorrect. Cannot change password.");
        }
    }
}
