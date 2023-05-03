using MoneyManagement.Service.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto> AuthentificateAsync(string email, string password);
    }
}
