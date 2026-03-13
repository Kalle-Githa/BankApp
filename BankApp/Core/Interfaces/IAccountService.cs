using BankApp.Data.DTO;
using BankApp.Data.Entities;

namespace BankApp.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsByCustomerIdForValidation(int customerId);

        Task<IEnumerable<AccountOverviewDTO>> GetAccountsByCustomerId(int customerId);
        Task CreateAccount(int customerId, int accountTypeId);
        Task Deposit(int accountId, decimal amount);
        Task Transfer(int customerId, TransferDTO dto);
        Task<IEnumerable<TransactionOverviewDTO>> GetTransactionsForAccount(int customerId, int accountNumber);



    }
}
