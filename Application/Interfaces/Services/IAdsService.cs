using EMarketApp.Core.Application.ViewModels;

namespace EMarketApp.Core.Application.Interfaces.Services
{
    public interface IAdsService : IGenericService<AdViewModel, SaveAdViewModel>
    {
        Task<List<AdViewModel>> GetAllViewModelWithFilters(FilterViewModel vm);
        Task<DetailsAdViewModel> GetAdDetailsByIdAsync(int id);
        List<AdViewModel> FilterAdsByCategory(List<AdViewModel> adViewModelList, string categoryId);
    }
}
