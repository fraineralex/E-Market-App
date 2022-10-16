using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Domain.Entities;
using EMarketApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMarketApp.Infrastructure.Persistence.Repositories
{
    public class AdRepository : GenericRepository<Ads>, IAdsRepository
    {
        private readonly ApplicationContext _dbContext;

        public AdRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /*public async Task AddAsync(Categories category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Categories category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Categories category)
        {
            _dbContext.Set<Categories>().Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            return await _dbContext.Set<Categories>().ToListAsync();
        }
        public async Task<Categories> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Categories>().FindAsync(id);
        }*/
    }
}
