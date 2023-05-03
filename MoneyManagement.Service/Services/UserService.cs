using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Entities.Configurations;
using MoneyManagement.Service.DTOs.User;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Extensions;
using MoneyManagement.Service.Helpers;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> repository;

        public UserService(IMapper mapper, IRepository<User> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async ValueTask<UserResultDto> CreateAsync(UserCreationDto dto)
        {
            var user = await this.repository.SelectAsync(x => x.Email == dto.Email && x.IsDeleted == false);
            if (user is not null)
            {
                throw new CustomException(409, "User Already Exists");
            }
            var mapped = this.mapper.Map<User>(dto);
            mapped.CreatedAt = DateTime.Now;
            mapped.Password = PasswordHasher.Hash(mapped.Password);
            var result = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();
            return this.mapper.Map<UserResultDto>(result);

        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var user = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (user is null)
            {
                throw new CustomException(404, "Not Found");
            }
            await this.repository.DeleteAsync(user);
            await this.repository.SaveAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserResultDto>> GetAll(PaginationParamas @params)
        {
            var res = await this.repository.SelectAll(x => x.IsDeleted == false).ToPagedList(@params).ToListAsync();
            return this.mapper.Map<IEnumerable<UserResultDto>>(res);
        }

        public async ValueTask<User> GetByEmail(string email)
        {
            return await this.repository.SelectAsync(x => x.Email == email && x.IsDeleted == false);
        }

        public async ValueTask<UserResultDto> GetById(long id)
        {
            var res = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (res is null) throw new CustomException(404, "Not Found");
            return this.mapper.Map<UserResultDto>(res);
        }

        public async ValueTask<UserResultDto> UpdateAsync(long id, UserCreationDto dto)
        {
            var res = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (res is null)
            {
                throw new CustomException(404, "Not Found");
            }
            var mapped = this.mapper.Map<User>(dto);
            mapped.UpdatedAt = DateTime.Now;
            await this.repository.SaveAsync();
            return this.mapper.Map<UserResultDto>(mapped);

        }

        public async ValueTask<UserResultDto> UploadImageAsync(long id, IFormFile file)
        {
            var user = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (user is null)
                throw new CustomException(404, "Not found");

            if (file is null || file.Length == 0)
                throw new CustomException(400, "Invalid file");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new CustomException(400, "Invalid file type");

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine("uploads", fileName);
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            user.ImagePath = absolutePath;
            await this.repository.SaveAsync();

            return this.mapper.Map<UserResultDto>(user);
        }


        public async ValueTask<UserResultDto> UserRoleUpdateAsync(long id, UserRoleUpdate dto)
        {
            var user = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            if (user is null)
                throw new CustomException(404, "User is not found");

            user.Role = dto.Role;
            await this.repository.SaveAsync();

            return this.mapper.Map<UserResultDto>(user);
        }
    


    } 
}
