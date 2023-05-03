using AutoMapper;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Expense;
using MoneyManagement.Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCreationDto>().ReverseMap();
            CreateMap<User, UserResultDto>().ReverseMap();
            CreateMap<User, UserRoleUpdate>().ReverseMap();
            CreateMap<User, UserResultDto>().ReverseMap();


            CreateMap<Expense, ExpenseCreationDto>().ReverseMap();
            CreateMap<Expense, ExpenseResultDto>().ReverseMap();

        }
    }
}
