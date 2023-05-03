using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs.Login;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentificate(LoginDto dto) =>
            Ok(await this._authService.AuthentificateAsync(dto.Email, dto.Password));
    }
}
