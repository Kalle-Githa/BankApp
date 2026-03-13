namespace BankApp.Data.DTO
{
    public class TransactionOverviewDTO
    {
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!;
        public string Operation { get; set; } = null!;

    }
}
