using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public CustomerController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("accounts")]
        public async Task<IActionResult> GetMyAccounts()
        {
            // Hämta CustomerId från JWT
            var customerIdClaim = User.FindFirst("CustomerId");

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
                return Unauthorized("Ogiltig kund, kontakta support");

            var accounts = await _accountService.GetAccountsByCustomerId(customerId);

            return Ok(accounts);
        }

        [HttpGet("accounts/{accountNumber}/transactions")]
        public async Task<IActionResult> GetTransactions(int accountNumber)
        {
            var customerIdClaim = User.FindFirst("CustomerId");

            if (customerIdClaim == null)
                return Unauthorized();

            int customerId = int.Parse(customerIdClaim.Value);

            var transactions =
                await _accountService.GetTransactionsForAccount(
                    customerId,
                    accountNumber
                );

            return Ok(transactions);
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(CreateAccountDTO dto)
        {
            // Hämta CustomerId från JWT
            var customerIdClaim = User.FindFirst("CustomerId");

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
                return Unauthorized("Ogiltig kund, kontakta support");

            await _accountService.CreateAccount(customerId, dto.AccountTypeId);

            return Ok("Kontot skapades");
        }



        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferDTO dto)
        {
            // Hämta CustomerId från JWT
            var customerIdClaim = User.FindFirst("CustomerId");

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
                return Unauthorized("Ogiltig kund, kontakta support");

            await _accountService.Transfer(customerId, dto);

            return Ok("Överföring genomförd");
        }



    }
}
