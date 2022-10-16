using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;

namespace EMarketApp.Core.Application.Services
{
    public class EntitiesService : IEntitiesService
    {
        private readonly IAdsService _adService;
        private readonly ICategoryService _categoryService;

        public EntitiesService(IAdsService adService, ICategoryService categoryService)
        {
            _adService = adService;
            _categoryService = categoryService;
        }

        public async Task<EntitiesViewModel> GetEntitiesViewModel()
        {
            return new EntitiesViewModel()
            {
                AdsList = await _adService.GetAllViewModel(),
                CategoriesList = await _categoryService.GetAllViewModel(),
            };
        }
    }
}
