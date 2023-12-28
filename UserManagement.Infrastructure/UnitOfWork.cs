using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Interfaces.IRepositories;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members

        private readonly UserManagementDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public IUserRepository UserRepository { get; set; }

        #endregion

        #region Constructors

        public UnitOfWork(UserManagementDbContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
        }

        #endregion



        #region Methods

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _dbContext.Dispose();
        }

        #endregion
    }
}
