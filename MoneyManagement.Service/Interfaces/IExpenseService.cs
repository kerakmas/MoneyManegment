using Microsoft.AspNetCore.Http;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManagement.Service.DTOs.Expense;

namespace MoneyManagement.Service.Interfaces
{
    public interface IExpenseService
    {
        ValueTask<ExpenseResultDto> CreateAsync(ExpenseCreationDto dto);
        ValueTask<ExpenseResultDto> UpdateAsync(long id, ExpenseCreationDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<ExpenseResultDto> GetById(long id);
        ValueTask<IEnumerable<ExpenseResultDto>> GetAllAsync(PaginationParamas @params);
        ValueTask<IEnumerable<ExpenseResultDto>> GetAllByUserIdAsync(long userid);
        ValueTask<IEnumerable<ExpenseResultDto>> GetByDateAsync(long id,DateTime from, DateTime to);
        ValueTask<long> GetAllUsage(long id, DateTime from, DateTime to);
    }
}
