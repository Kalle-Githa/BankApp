using BankApp.Data.Entities;
using BankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.Repos
{


    public class AccountRepo : IAccountRepo
    {
        private readonly BankAppDataContext _context;

        public AccountRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Include(d => d.Account)
                .ThenInclude(a => a.AccountTypes)
                .Select(d => d.Account)
                .Distinct()
                .ToListAsync();

        }

        public async Task<Account?> GetByIdAsync(int accountId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
