using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Entities;
using BankApp.Data.Interfaces;

namespace BankApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IDispositionRepo _dispositionRepo;
        private readonly ITransactionRepo _transactionRepo;

        public AccountService(IAccountRepo accountRepo, IDispositionRepo dispositionRepo, ITransactionRepo transactionRepo)
        {
            _accountRepo = accountRepo;
            _dispositionRepo = dispositionRepo;
            _transactionRepo = transactionRepo;
        }



        public async Task<IEnumerable<AccountOverviewDTO>> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await _accountRepo.GetByCustomerIdAsync(customerId);

            return accounts.Select(a => new AccountOverviewDTO
            {
                AccountNumber = a.AccountId,
                AccountType = a.AccountTypes!.TypeName,
                Balance = a.Balance
            });

        }
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdForValidation(int customerId)
        {
            return await _accountRepo.GetByCustomerIdAsync(customerId);
        }


        public async Task CreateAccount(int customerId, int accountTypeId)
        {
            var account = new Account
            {
                AccountTypesId = accountTypeId,
                Balance = 0,
                Created = DateOnly.FromDateTime(DateTime.Now),
                Frequency = "Monthly"
            };

            await _accountRepo.AddAsync(account);

            var disposition = new Disposition
            {
                CustomerId = customerId,
                AccountId = account.AccountId,
                Type = "OWNER"
            };

            await _dispositionRepo.AddAsync(disposition);


        }

        public async Task Deposit(int accountId, decimal amount)
        {
            var account = await _accountRepo.GetByIdAsync(accountId);
            if (account == null)
                throw new InvalidOperationException("Kontot finns inte");

            account.Balance += amount;

            await _accountRepo.SaveChangesAsync();


        }

        public async Task Transfer(int customerId, TransferDTO dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Beloppet måste vara större än 0");

            var myAccounts =
                await GetAccountsByCustomerIdForValidation(customerId);

            var fromAccount =
                myAccounts.FirstOrDefault(a => a.AccountId == dto.MyAccountNumber);

            if (fromAccount == null)
                throw new InvalidOperationException("Kontot tillhör inte kunden");

            var toAccount =
                await _accountRepo.GetByIdAsync(dto.ToAccountNumber);

            if (toAccount == null)
                throw new InvalidOperationException("Mottagarkontot finns inte");

            if (fromAccount.Balance < dto.Amount)
                throw new InvalidOperationException("Otillräckligt saldo");

            fromAccount.Balance -= dto.Amount;
            toAccount.Balance += dto.Amount;

            await _transactionRepo.AddAsync(new Transaction
            {
                AccountId = fromAccount.AccountId,
                Amount = -dto.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Debit",
                Operation = "Withdrawal in Cash"
            });

            await _transactionRepo.AddAsync(new Transaction
            {
                AccountId = toAccount.AccountId,
                Amount = dto.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Credit",
                Operation = "Credit in Cash"
            });

            await _accountRepo.SaveChangesAsync();
        }
        public async Task<IEnumerable<TransactionOverviewDTO>> GetTransactionsForAccount(int customerId, int accountNumber)

        {

            var myAccounts =
                await GetAccountsByCustomerIdForValidation(customerId);

            var account =
                myAccounts.FirstOrDefault(a => a.AccountId == accountNumber);

            if (account == null)
                throw new InvalidOperationException("Kontot tillhör inte kunden");


            var transactions =
                await _transactionRepo.GetByAccountIdAsync(accountNumber);


            return transactions.Select(t => new TransactionOverviewDTO
            {
                Date = t.Date,
                Amount = t.Amount,
                Type = t.Type,
                Operation = t.Operation
            });
        }

    }
}
