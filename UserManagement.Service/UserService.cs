using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.Interfaces.IRepositories;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Service.IServices;
using UserManagement.Shared.Models;

namespace UserManagement.Service
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<ApplicationUser> AddUserAsync(UserDto model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await _userRepository.AddAsync(user);

            // Perform additional user-related operations if needed

            return result;
        }

        public async Task BlockUserAsync(string userId)
        {
            await _userRepository.BlockUserAsync(userId);
        }
        public async Task ManageUserRolesAsync(string userId, List<string> roleNames)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                await _userRepository.ManageUserRolesAsync(user, roleNames);
            }
        }
        public bool IsExist(LoginDto model)
        {
            return _userRepository.GetByCondition(x => x.UserName == model.Username && x.PasswordHash == model.Password).FirstOrDefault() != null ? true : false;
        }
        //public async Task ManageRolePermissionsAsync(string roleName, List<string> permissions)
        //{
        //    var role = await _roleManager.FindByNameAsync(roleName);

        //    if (role != null)
        //    {
        //        await _userRepository.ManageRolePermissionsAsync(role, permissions);
        //    }
        //}

    }
}
