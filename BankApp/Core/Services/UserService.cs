using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Entities;
using BankApp.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepo userRepo, ICustomerRepo customerRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _customerRepo = customerRepo;
            _configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            if (user.CustomerId != null)
            {
                claims.Add(
                    new Claim("CustomerId", user.CustomerId.Value.ToString())
                );
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var signingCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpiresInMinutes"])
                ),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string?> Login(LoginDTO dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.UserName);

            if (user == null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                return null;

            return GenerateToken(user);
        }

        public async Task CreateCustomerUser(int customerId, CreateUserDTO dto)
        {

            var customer = await _customerRepo.GetByIdAsync(customerId);
            if (customer == null)
                throw new ArgumentException("Kund finns inte");


            var existingUserForCustomer =
                await _userRepo.GetByCustomerIdAsync(customerId);

            if (existingUserForCustomer != null)
                throw new InvalidOperationException(
                    "Kund har redan ett konto");


            var userNameAvailable =
                await _userRepo.GetByUsernameAsync(dto.Username);

            if (userNameAvailable != null)
                throw new InvalidOperationException(
                    "Användarnamn är redan upptaget");


            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    dto.TemporaryPassword),
                Role = "Customer",
                CustomerId = customerId
            };


            await _userRepo.AddUserAsync(user);
            await _userRepo.SaveChangesAsync();
        }
    }
}
