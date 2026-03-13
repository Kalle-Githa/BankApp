using BankApp.Data.DTO;

namespace BankApp.Core.Interfaces
{
    public interface ILoanService
    {
        Task CreateLoan(int customerId, CreateLoanDTO dto);

    }
}
