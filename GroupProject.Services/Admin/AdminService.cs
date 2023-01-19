using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Admin;

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

            _dbContext.Admins.Add(entity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
        
    }
}