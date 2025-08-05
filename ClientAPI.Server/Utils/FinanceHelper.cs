namespace ClientAPI.Server.Utils
{
    public class FinanceHelper
    {
        public static decimal GetInterestRate(decimal deposit)
        {
            if (deposit <= 5000) return 0.0005m;
            else if (deposit <= 25000) return 0.00075m;
            else return 0.001m;
        }
    }
}
