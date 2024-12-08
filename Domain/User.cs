namespace Domain
{
    public class User : byAll
    {
        public int Id { get; set; } // Primary key
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
