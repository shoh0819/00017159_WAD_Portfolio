namespace _00017159_WAD_Portfolio.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Feedback> feedbacks { get; set; }
    }
}
