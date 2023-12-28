using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManagement.Shared.Models;

namespace UserManagement.Infrastructure
{
    public class UserManagementDbContext:IdentityDbContext<ApplicationUser>
    {
        #region Constructors

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {
        }

        #endregion
    }
}
