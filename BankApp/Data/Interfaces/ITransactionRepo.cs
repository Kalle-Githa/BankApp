using BankApp.Data.Entities;


namespace BankApp.Data.Interfaces
{
    public interface ITransactionRepo
    {
        Task AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);

    }
}
