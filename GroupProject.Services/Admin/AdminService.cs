using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Admin;
using Microsoft.AspNetCore.Identity;

namespace GroupProject.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> RegisterAdminAsync(AdminCreate newAdmin)
        {
            var entity = new AdminEntity {
                UserName = newAdmin.Username,
                Password = newAdmin.Password
            };
            var passwordHasher = new PasswordHasher<AdminEntity>();
            entity.Password = passwordHasher.HashPassword(entity, newAdmin.Password);
            _dbContext.Admins.Add(entity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
        public async Task<AdminDetail> GetAdminByIdAsync(int Id)
        {
            var entity = await _dbContext.Admins.FindAsync(Id);
            if (entity is null)
                return null;
            var adminDetail = new AdminDetail {
                Id = entity.Id,
                Username = entity.UserName
            };
            return adminDetail;
        }
        public async Task<bool> UpdateAdminAsync(AdminUpdate model)
        {
            var foundAdmin = await _dbContext.Admins.FindAsync(model.Id);
            if (foundAdmin is null)
                return false;
            if (model.Username is not null)
                foundAdmin.UserName = model.Username;
            if (model.Password is not null)
            {
                var passwordHasher = new PasswordHasher<AdminEntity>();
                foundAdmin.Password = passwordHasher.HashPassword(foundAdmin, model.Password);
            }
            return (await _dbContext.SaveChangesAsync() == 1);
        }
    }
}