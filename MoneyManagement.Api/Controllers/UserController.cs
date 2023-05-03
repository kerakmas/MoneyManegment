using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Service.DTOs.User;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserCreationDto dto) =>
            Ok(await this.userService.CreateAsync(dto));
        [HttpDelete("{id}"), AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(long id) =>
            Ok(await this.userService.DeleteAsync(id));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) =>
            Ok(await this.userService.GetById(id));
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParamas @params) =>
            Ok(await this.userService.GetAll(@params));
        [HttpPut]
        public async Task<IActionResult> UpdateAync(long id, UserCreationDto dto) =>
            Ok(await this.userService.UpdateAsync(id, dto));
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(long id,  IFormFile file) =>
            Ok(await this.userService.UploadImageAsync(id, file));
        [HttpPut("role"),  Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(long id, UserRoleUpdate dto) =>
            Ok(await this.userService.UserRoleUpdateAsync(id, dto));
        
    }
}
