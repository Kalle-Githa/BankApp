using BankApp.Data.Entities;
using BankApp.Data.Interfaces;

namespace BankApp.Data.Repos
{
    public class DispositionRepo : IDispositionRepo
    {
        private readonly BankAppDataContext _context;

        public DispositionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Disposition disposition)
        {
            await _context.Dispositions.AddAsync(disposition);
            await _context.SaveChangesAsync();
        }

    }
}
