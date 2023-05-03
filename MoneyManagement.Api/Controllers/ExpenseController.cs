using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Service.DTOs.Expense;
using MoneyManagement.Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAysnc(ExpenseCreationDto dto) =>
            Ok(await this.expenseService.CreateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id) =>
            Ok(await this.expenseService.DeleteAsync(id));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBydId(long id) =>
            Ok(await this.expenseService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParamas @params) =>
            Ok(await this.expenseService.GetAllAsync(@params));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, ExpenseCreationDto dto) =>
            Ok(await this.expenseService.UpdateAsync(id, dto));

        [HttpGet("user/{userid}/date")]
        public async Task<IActionResult> GetAllByDate(long userid, [FromQuery][DataType(DataType.Date)] DateTime from, [FromQuery][DataType(DataType.Date)] DateTime to) =>
         Ok(await this.expenseService.GetByDateAsync(userid, from, to));


        [HttpGet("user/{userid}/total")]
        public async Task<ActionResult<long>> GetTotalUsage(long userid, [FromQuery] DateTime from, [FromQuery] DateTime to) =>
            Ok(await this.expenseService.GetAllUsage(userid, from, to));
    }
}
