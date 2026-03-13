using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly ILoanService _loanService;

        public AdminController(IUserService userservice, ICustomerService customerService, ILoanService loanService)
        {
            _userService = userservice;
            _customerService = customerService;
            _loanService = loanService;

        }

        [HttpGet("admincheck")]
        public IActionResult Admincheck() // Simpel kontroll 
        {
            return Ok("Du är ADMIN");
        }

        [HttpPost("customers/{customerId}/user")]
        public async Task<IActionResult> CreateCustomerUser(int customerId, CreateUserDTO dto)
        {

            await _userService.CreateCustomerUser(customerId, dto);
            return Ok("Användare skapad");

        }

        [HttpPost("customers/create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO dto)
        {
            var customerId = await _customerService.CreateCustomer(dto);
            return Ok(new { CustomerId = customerId });
        }


        [HttpPost("customers/{customerId}/grant-loan")]
        public async Task<IActionResult> CreateLoan(int customerId, CreateLoanDTO dto)
        {
            await _loanService.CreateLoan(customerId, dto);

            return Ok("Lån skapat och belopp insatt på konto");
        }


    }
}
