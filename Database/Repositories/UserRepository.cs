using EMarketApp.Core.Application.Helpers;
using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Application.ViewModels;
using EMarketApp.Core.Domain.Entities;
using EMarketApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMarketApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUsersRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(Users entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
        }

        public async Task<Users> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypy = PasswordEncryption.ComputeSha256Hash(loginVm.Password);

            Users user = await _dbContext.Set<Users>()
                .FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypy);

            return user;
        }
    }
}
