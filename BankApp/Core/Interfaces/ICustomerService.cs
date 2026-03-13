using BankApp.Data.DTO;

namespace BankApp.Core.Interfaces
{

    public interface ICustomerService
    {
        Task<int> CreateCustomer(CreateCustomerDTO dto);
    }


}
