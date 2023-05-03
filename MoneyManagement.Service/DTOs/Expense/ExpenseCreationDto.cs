﻿using MoneyManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.DTOs.Expense
{
    public class ExpenseCreationDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public PaymentType Type { get; set; }
    }
}
