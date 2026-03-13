using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _userService.Login(dto);

            if (token == null)
                return Unauthorized("Fel användarnamn eller lösenord");

            return Ok(new { token });
        }

    }
}
