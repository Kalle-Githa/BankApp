using BankApp.Data.Entities;
using BankApp.Data.Interfaces;

namespace BankApp.Data.Repos
{
    public class LoanRepo : ILoanRepo
    {

        private readonly BankAppDataContext _context;

        public LoanRepo(BankAppDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }
    }
}
