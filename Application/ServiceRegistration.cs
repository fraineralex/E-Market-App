using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EMarketApp.Core.Application
{
    //Extension method - decorator pattern
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region services
            services.AddTransient<IAdsService, AdService>();
            services.AddTransient<ICategoryService, CategoryService>();
            #endregion
        }
    }
}
