using System.Security.Cryptography;
using System.Text;
using Domain;
using dataHandeling;

namespace MoneyFlow
{
    public class PasswordManager
    {
        private readonly DataHandeling _context;

        public PasswordManager(DataHandeling context)
        {
            _context = context;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute SHA-256 hash for the password
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                }
                return builder.ToString();
            }
        }

        public User RegisterUser()
        {
            Console.WriteLine("Create your account:");
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Hash the user's password before storing it
            string passwordHash = HashPassword(password);

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = passwordHash,
                dateTime = DateTime.Now
            };

            _context.Users.Add(user); // Add the new user to the database
            _context.SaveChanges();

            Console.WriteLine("Account created successfully.");
            return user;
        }

        public User AuthenticateUser()
        {
            Console.WriteLine("Login:");
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Hash the input password to compare with the stored hash
            string passwordHash = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == passwordHash);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
            }

            return user;
        }
    }
}
