using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Domain.Entities
{
    public class User : Auditable
    {
        public string FullName { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }
        public bool Verified { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public string? ImagePath { get; set; }

    }
}
