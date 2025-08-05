namespace ClientAPI.Server.Data.DTOs.ClientDTOs
{
    public class ClientDetailsDTO
    {
        public Guid UserId { get; set; }
        public int ClientId { get; set; }

        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime Birthday { get; set; }
        public int Age { get; set; }

        public decimal SavingsDeposit { get; set; }
        public decimal SavingsInterest { get; set; }
        public decimal NetSavingsInterest { get; set; }
    }
}
