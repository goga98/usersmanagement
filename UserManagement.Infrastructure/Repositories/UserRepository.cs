using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Interfaces.IRepositories;
using UserManagement.Shared.Models;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManagementDbContext _context;
        public UserRepository(UserManagementDbContext context) : base(context)
        {
        }

        // Add specific methods related to user management
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task BlockUserAsync(string userId)
        {
            var user = await GetByIdAsync(userId);

            if (user != null)
            {
                user.LockoutEnd = DateTimeOffset.MaxValue;
                await UpdateAsync(user);
            }
        }
        public async Task ManageUserRolesAsync(ApplicationUser user, List<string> roleNames)
        {
            var existingRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = existingRoles.Except(roleNames).ToList();
            var rolesToAdd = roleNames.Except(existingRoles).ToList();

            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        //public async Task ManageRolePermissionsAsync(ApplicationRole role, List<string> permissions)
        //{
        //    var existingPermissions = await _roleManager.GetClaimsAsync(role);
        //    var permissionsToRemove = existingPermissions
        //        .Where(claim => claim.Type == "Permission" && !permissions.Contains(claim.Value))
        //        .ToList();

        //    var permissionsToAdd = permissions
        //        .Where(permission => !existingPermissions.Any(claim => claim.Type == "Permission" && claim.Value == permission))
        //        .Select(permission => new Claim("Permission", permission))
        //    .ToList();

        //    await _roleManager.RemoveClaimsAsync(role, permissionsToRemove);
        //    await _roleManager.AddClaimsAsync(role, permissionsToAdd);
        //}
    }
}
