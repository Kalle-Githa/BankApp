using BankApp.Data.DTO;

namespace BankApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<string?> Login(LoginDTO dto);
        Task CreateCustomerUser(int customerId, CreateUserDTO dto);
    }
}
