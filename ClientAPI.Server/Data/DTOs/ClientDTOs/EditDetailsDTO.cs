namespace ClientAPI.Server.Data.DTOs.ClientDTOs
{
    public class EditDetailsDTO
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public decimal SavingsDeposit { get; set; }
    }
}
