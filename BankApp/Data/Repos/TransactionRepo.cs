using BankApp.Data.Entities;
using BankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankAppDataContext _context;

        public TransactionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

    }
}
