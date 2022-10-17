using EMarketApp.Core.Application.ViewModels;

namespace EMarketApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel>
        where ViewModel : class
        where SaveViewModel : class
    {
        Task<List<ViewModel>> GetAllViewModel();
        Task Add(SaveViewModel vm);
        Task Update(SaveViewModel vm);
        Task Delete(int id);
        Task<SaveViewModel> GetSaveViewModelById(int id);


    }
}
