using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Service.DTOs.Expense;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Extensions;
using MoneyManagement.Service.Helpers;
using MoneyManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> repository;
        private readonly IMapper mapper;

        public ExpenseService(IRepository<Expense> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async ValueTask<ExpenseResultDto> CreateAsync(ExpenseCreationDto dto)
        {
            var expense = await this.repository.SelectAsync(x => x.Name == dto.Name && x.IsDeleted == false);
            if (expense is not null)
            {
                throw new CustomException(409, "User Already Exists");
            }
            var mapped = this.mapper.Map<Expense>(dto);
            mapped.CreatedAt = DateTime.Now;
            var result = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();
            return this.mapper.Map<ExpenseResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var expense = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (expense is null)
            {
                throw new CustomException(404, "Not Found");
            }
            await this.repository.DeleteAsync(expense);
            await this.repository.SaveAsync();
            return true;
        }

        public async ValueTask<IEnumerable<ExpenseResultDto>> GetAllAsync(PaginationParamas @params)
        {
            var res = await this.repository.SelectAll(x => x.IsDeleted == false).ToPagedList(@params).ToListAsync();
            return this.mapper.Map<IEnumerable<ExpenseResultDto>>(res);
        }

        public async ValueTask<ExpenseResultDto> GetById(long id)
        {
            var res = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (res is null) throw new CustomException(404, "Not Found");
            return this.mapper.Map<ExpenseResultDto>(res);
        }

        public async ValueTask<ExpenseResultDto> UpdateAsync(long id, ExpenseCreationDto dto)
        {
            var res = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (res is null)
            {
                throw new CustomException(404, "Not Found");
            }
            var mapped = this.mapper.Map<User>(dto);
            mapped.UpdatedAt = DateTime.Now;
            await this.repository.SaveAsync();
            return this.mapper.Map<ExpenseResultDto>(mapped);
        }
        public async ValueTask<IEnumerable<ExpenseResultDto>> GetAllByUserIdAsync(long userid) 
        {
            var res = await this.repository.SelectAll(x => x.UserId == userid).ToListAsync();
            return this.mapper.Map<IEnumerable<ExpenseResultDto>>(res);
        }
        public async ValueTask<IEnumerable<ExpenseResultDto>> GetByDateAsync(long id, DateTime from, DateTime to)
        {
            var res = await this.repository.SelectAll(x => x.CreatedAt >= from && x.CreatedAt <= to && x.UserId == id).ToListAsync();
            return this.mapper.Map<IEnumerable<ExpenseResultDto>>(res);
           
        }
        public async ValueTask<long> GetAllUsage(long id,DateTime from, DateTime to)
        {
            long total = 0;
            var result = await this.GetByDateAsync(id, from, to);
            foreach(var item in result)
            {
                total += item.Price;
            }
            return total;

        }
        



    }
}
