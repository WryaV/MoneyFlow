namespace Domain
{
    public class Flow : byAll
    {
        public int Id { get; set; } // Primary key

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; } = null!; // Navigation property

        public int Income { get; set; }
        public int Expense { get; set; }
        public int Tax { get; set; }
    }
}
