
namespace ClientAPI.Server.Data.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Birthday { get; set; }

        [Range(1000, double.MaxValue, ErrorMessage = "Minimum deposit is 1000.00")]
        public decimal SavingsDeposit { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
