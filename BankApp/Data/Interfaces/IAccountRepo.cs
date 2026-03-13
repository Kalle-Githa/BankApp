using BankApp.Data.Entities;

namespace BankApp.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId);
        Task<Account?> GetByIdAsync(int accountId);

        Task AddAsync(Account account);
        Task SaveChangesAsync();
    }
}
