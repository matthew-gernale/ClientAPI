namespace ClientAPI.Server.Data.DTOs.ClientDTOs
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal SavingsDeposit { get; set; }
    }
}
