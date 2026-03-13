namespace BankApp.Data.DTO
{
    public class TransferDTO
    {
        public int MyAccountNumber { get; set; }
        public int ToAccountNumber { get; set; }
        public decimal Amount { get; set; }

    }
}
