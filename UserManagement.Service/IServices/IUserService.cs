using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Shared.Models;

namespace UserManagement.Service.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<ApplicationUser> AddUserAsync(UserDto model);

        Task BlockUserAsync(string userId);
        Task ManageUserRolesAsync(string userId, List<string> roleNames);
        //Task InsertUser(InsertDto model);
        //Task UpdateUser(UpdateUser model);
        bool IsExist(LoginDto model);
        //Task InitializeRoles(UserManager<UserDto> userManager, RoleManager<IdentityRole> roleManager);
    }
}
