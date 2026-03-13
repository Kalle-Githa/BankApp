using BankApp.Data.Entities;

namespace BankApp.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer?> GetByIdAsync(int customerId);
        Task AddAsync(Customer customer);
    }
}
