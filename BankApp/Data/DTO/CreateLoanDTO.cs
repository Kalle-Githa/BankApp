namespace BankApp.Data.DTO
{
    public class CreateLoanDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
    }
}
