using EMarketApp.Core.Application.ViewModels;
using EMarketApp.Core.Domain.Entities;

namespace EMarketApp.Core.Application.Interfaces.Repositories
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<Users> LoginAsync(LoginViewModel loginVm);
    }
}
