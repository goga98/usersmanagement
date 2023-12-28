using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Shared.Models;

namespace UserManagement.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {

        Task BlockUserAsync(string userId);
        Task ManageUserRolesAsync(ApplicationUser user, List<string> roleNames);
    }
}
