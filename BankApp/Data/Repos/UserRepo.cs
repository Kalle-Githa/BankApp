using BankApp.Data.Entities;
using BankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly BankAppDataContext _context;

        public UserRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.CustomerId == customerId);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
