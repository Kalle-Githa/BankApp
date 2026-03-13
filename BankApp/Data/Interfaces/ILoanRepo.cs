using BankApp.Data.Entities;

namespace BankApp.Data.Interfaces
{
    public interface ILoanRepo
    {
        Task AddAsync(Loan loan);
    }
}
