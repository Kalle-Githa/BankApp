using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Entities;
using BankApp.Data.Interfaces;

namespace BankApp.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepo _loanRepo;
        private readonly IAccountService _accountService;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAccountRepo _accountRepo;



        public LoanService(ILoanRepo loanRepo, IAccountService accountService, ITransactionRepo transactionRepo, IAccountRepo accountRepo)
        {
            _loanRepo = loanRepo;
            _accountService = accountService;
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;

        }

        public async Task CreateLoan(int customerId, CreateLoanDTO dto)
        {

            var accounts = await _accountService.GetAccountsByCustomerIdForValidation(customerId);

            if (!accounts.Any(a => a.AccountId == dto.AccountId))
                throw new InvalidOperationException("Kontot tillhör inte kunden");


            if (dto.Amount <= 0)
                throw new ArgumentException("Lånebelopp måste vara större än 0");

            if (dto.Duration <= 0)
                throw new ArgumentException("Duration måste vara större än 0");

            var loan = new Loan
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Duration = dto.Duration,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Status = "Active",
                Payments = dto.Amount / dto.Duration
            };

            await _loanRepo.AddAsync(loan);
            await _accountService.Deposit(dto.AccountId, dto.Amount);

            await _transactionRepo.AddAsync(new Transaction
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Credit",
                Operation = "Loan"
            });
            await _accountRepo.SaveChangesAsync();






        }
    }
}
