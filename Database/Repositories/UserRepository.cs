using EMarketApp.Core.Application.Interfaces.Repositories;
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
    }
}
