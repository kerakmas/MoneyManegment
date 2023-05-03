using Microsoft.AspNetCore.Http;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserResultDto> CreateAsync(UserCreationDto dto);
        ValueTask<UserResultDto> UpdateAsync(long id, UserCreationDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<UserResultDto> GetById(long id);
        ValueTask<User> GetByEmail(string email);
        ValueTask<IEnumerable<UserResultDto>> GetAll(PaginationParamas @params);
        ValueTask<UserResultDto> UploadImageAsync(long id, IFormFile file);
        ValueTask<UserResultDto> UserRoleUpdateAsync(long id, UserRoleUpdate dto);
     
    }
}
