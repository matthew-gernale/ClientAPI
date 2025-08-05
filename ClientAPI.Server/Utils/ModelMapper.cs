namespace ClientAPI.Server.Utils
{
    public class ModelMapper
    {
        private const decimal TAX_RATE = 0.005m;

        public static ClientDetailsDTO ToClientDetailsDTO(Client dbClient)
        {
            var interestRate = FinanceHelper.GetInterestRate(dbClient.SavingsDeposit);
            var savingsInterest = dbClient.SavingsDeposit * interestRate;

            return new ClientDetailsDTO
            {
                UserId = dbClient.UserId,
                ClientId = dbClient.Id,
                Email = dbClient.User.Email,
                FirstName = dbClient.User.FirstName,
                LastName = dbClient.User.LastName,
                MiddleName = dbClient.User.MiddleName,
                Birthday = dbClient.Birthday,
                Age = TimeUtils.GetAge(dbClient.Birthday),
                SavingsDeposit = dbClient.SavingsDeposit,
                SavingsInterest = savingsInterest,
                NetSavingsInterest = savingsInterest - (savingsInterest * TAX_RATE),
            };
        }

        public static ClientDTO ToClientDTO(Client dbClient)
        {
            return new ClientDTO
            {
                ClientId = dbClient.Id,
                Email = dbClient.User.Email,
                ClientName = $"{dbClient.User.FirstName} {dbClient.User.LastName}",
                Age = TimeUtils.GetAge(dbClient.Birthday),
                SavingsDeposit = dbClient.SavingsDeposit
            };
        }
    }
}
