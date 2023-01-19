using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Admin;

namespace GroupProject.Services.Admin
{
    public interface IAdminService
    {
        Task<bool> RegisterAdminAsync(AdminCreate newAdmin);
        Task<AdminDetail> GetAdminByIdAsync(int Id);
        // Task<IEnumerable<AdminEntity>> GetAllAdminsAsync();
        // Task<bool> UpdateAdminAsync(AdminUpdate newAdminInfo);
        // Task<bool> DeleteAdminAsync(int Id);
    }
}