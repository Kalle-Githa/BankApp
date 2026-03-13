namespace BankApp.Data.DTO
{
    public class AccountOverviewDTO
    {
        public int AccountNumber { get; set; }

        public string AccountType { get; set; } = null!;
        public decimal Balance { get; set; }

    }
}
