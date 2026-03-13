using BankApp.Data.Entities;

namespace BankApp.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByCustomerIdAsync(int customerId);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();


    }
}
