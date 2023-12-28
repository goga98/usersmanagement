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
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(userId);
        }

        public async Task<ApplicationUser> AddUserAsync(UserDto model)
        {
            var user = new ApplicationUser { UserName = model.UserName, PasswordHash=model.Password, Email = model.Email };
            var result = await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task BlockUserAsync(string userId)
        {
            await _unitOfWork.UserRepository.BlockUserAsync(userId);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ManageUserRolesAsync(string userId, List<string> roleNames)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user != null)
            {
                await _unitOfWork.UserRepository.ManageUserRolesAsync(user, roleNames);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public bool IsExist(LoginDto model)
        {
            return _unitOfWork.UserRepository.GetByCondition(x => x.UserName == model.Username && x.PasswordHash == model.Password).FirstOrDefault() != null ? true : false;
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
