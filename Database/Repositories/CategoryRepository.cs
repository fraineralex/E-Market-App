using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Domain.Entities;
using EMarketApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMarketApp.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository :GenericRepository<Categories>, ICategoriesRepository
    {
        private readonly ApplicationContext _dbContext;

        public CategoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
