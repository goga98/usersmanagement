using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Service;
using UserManagement.Service.IServices;
using UserManagement.Shared.Models;

namespace UserManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto model)
        {
            try
            {
                var user = await _userService.AddUserAsync(model);
                return Ok(new { Message = "User created successfully.", UserId = user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to create user.", Error = ex.Message });
            }
        }

        [HttpPost("BlockUser/{userId}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            await _userService.BlockUserAsync(userId);
            return Ok(new { Message = "User blocked successfully." });
        }

        [HttpPost("ManageUserRoles/{userId}")]
        public async Task<IActionResult> ManageUserRoles(string userId, [FromBody] List<string> roleNames)
        {
            try
            {
                await _userService.ManageUserRolesAsync(userId, roleNames);
                return Ok(new { Message = "User roles updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to update user roles.", Error = ex.Message });
            }
        }

        //[HttpPost("ManageRolePermissions/{roleName}")]
        //public async Task<IActionResult> ManageRolePermissions(string roleName, [FromBody] List<string> permissions)
        //{
        //    var role = await _roleManager.FindByNameAsync(roleName);

        //    if (role == null)
        //    {
        //        return NotFound(new { Message = "Role not found." });
        //    }

        //    var existingPermissions = await _roleManager.GetClaimsAsync(role);
        //    var permissionsToRemove = existingPermissions
        //        .Where(claim => claim.Type == "Permission" && !permissions.Contains(claim.Value))
        //        .ToList();

        //    var permissionsToAdd = permissions
        //        .Where(permission => !existingPermissions.Any(claim => claim.Type == "Permission" && claim.Value == permission))
        //        .Select(permission => new Claim("Permission", permission))
        //        .ToList();

        //    await _roleManager.RemoveClaimAsync(role, permissionsToRemove);
        //    await _roleManager.AddClaimAsync(role, permissionsToAdd);

        //    return Ok(new { Message = "Role permissions updated successfully." });
        //}
    }
}
