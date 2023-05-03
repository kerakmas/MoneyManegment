using MoneyManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.DTOs.User
{
    public class UserResultDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public bool Verified { get; set; }
        public UserRole Role { get; set; }
    }
}
