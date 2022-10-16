using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using EMarketApp.Core.Domain.Entities;

namespace EMarketApp.Core.Application.Services
{
    public class AdService : IAdsService
    {
        private readonly IAdsRepository _adRepository;
        private readonly ICategoryService _categoryService;

        public AdService(IAdsRepository AdRepository, ICategoryService categoryService)
        {
            _adRepository = AdRepository;
            _categoryService = categoryService;
        }

        public async Task<List<AdViewModel>> GetAllViewModel()
        {
            var adsList = await _adRepository.GetAllAsync();
            return adsList.Select(ad => new AdViewModel
            {
                Id = ad.Id,
                Name = ad.Name,
                ImagePathOne = ad.ImagePathOne,
                ImagePathTwo = ad.ImagePathTwo,
                ImagePathThree = ad.ImagePathThree,
                ImagePathFour = ad.ImagePathFour,
                Price = ad.Price,
                Description = ad.Description,
                Category = ad.Categories.Name,

            }).ToList();
        }

        public async Task Add(SaveAdViewModel vm)
        {
            Ads ad = new();
            ad.Name = vm.Name;
            ad.ImagePathOne = vm.ImagePathOne;
            ad.ImagePathTwo = vm.ImagePathTwo;
            ad.ImagePathThree = vm.ImagePathThree;
            ad.ImagePathFour = vm.ImagePathFour;
            ad.Price = vm.Price;
            ad.Description = vm.Description;
            ad.CategoryId = vm.CategoryId;
            await _adRepository.AddAsync(ad);
        }

        public async Task<SaveAdViewModel> GetByIdSaveViewModel(int id)
        {
            var ad = await GetSaveViewModelById(id);

            SaveAdViewModel vm = new();
            vm.Id = ad.Id;
            vm.Name = ad.Name;
            vm.ImagePathOne = ad.ImagePathOne;
            vm.ImagePathTwo = ad.ImagePathTwo;
            vm.ImagePathThree = ad.ImagePathThree;
            vm.ImagePathFour = ad.ImagePathFour;
            vm.Price = ad.Price;
            vm.Description = ad.Description;
            vm.CategoryId = ad.CategoryId;
            vm.CategoriesList = await _categoryService.GetAllViewModel();

            return vm;
        }

        public async Task Update(SaveAdViewModel vm)
        {
            Ads ad = new();
            ad.Id = vm.Id;
            ad.Name = vm.Name;
            ad.ImagePathOne = vm.ImagePathOne;
            ad.ImagePathTwo = vm.ImagePathTwo;
            ad.ImagePathThree = vm.ImagePathThree;
            ad.ImagePathFour = vm.ImagePathFour;
            ad.Price = vm.Price;
            ad.Description = vm.Description;
            ad.CategoryId = vm.CategoryId;

            await _adRepository.UpdateAsync(ad);
        }

        public async Task Delete(int id)
        {
            var ad = await _adRepository.GetByIdAsync(id);
            await _adRepository.DeleteAsync(ad);
        }

        public async Task<SaveAdViewModel> GetSaveViewModelById(int id)
        {
            var ad = await _adRepository.GetByIdAsync(id);

            SaveAdViewModel vm = new()
            {
                Id = ad.Id,
                Name = ad.Name,
                ImagePathOne = ad.ImagePathOne,
                ImagePathTwo = ad.ImagePathTwo,
                ImagePathThree = ad.ImagePathThree,
                ImagePathFour = ad.ImagePathFour,
                Price = ad.Price,
                Description = ad.Description,
                CategoryId = ad.CategoryId,
            };

            return vm;
        }

        public async Task<List<AdViewModel>> GetAllViewModelWithFilters(FilterViewModel vm)
        {
            var adsList = await _adRepository.GetAllAsync();
            var adViewModelList = adsList.Select(ad => new AdViewModel
            {
                Id = ad.Id,
                Name = ad.Name,
                ImagePathOne = ad.ImagePathOne,
                ImagePathTwo = ad.ImagePathTwo,
                ImagePathThree = ad.ImagePathThree,
                ImagePathFour = ad.ImagePathFour,
                Price = ad.Price,
                Description = ad.Description,
                Category = ad.Categories.Name,
                CategoryId = ad.Categories.Id,

            }).ToList();

            if (vm.CategoryId != null)
            {
                adViewModelList = adViewModelList
                    .Where(ad => ad.CategoryId == vm.CategoryId.Value)
                    .ToList();
            }

            if (!String.IsNullOrEmpty(vm.AdName))
            {
                adViewModelList = adViewModelList
                    .Where(ad => ad.Name.Contains(vm.AdName))
                    .ToList();
            }

            if (vm.AdId != null)
            {
                adViewModelList = adViewModelList
                    .Where(ad => ad.Id == vm.AdId.Value)
                    .ToList();
            }

            return adViewModelList;
        }

        public async Task<AdViewModel> GetAdDetailsByIdAsync(int id)
        {
            var ad = await _adRepository.GetByIdAsync(id);

            AdViewModel vm = new()
            {
                Id = ad.Id,
                Name = ad.Name,
                ImagePathOne = ad.ImagePathOne,
                ImagePathTwo = ad.ImagePathTwo,
                ImagePathThree = ad.ImagePathThree,
                ImagePathFour = ad.ImagePathFour,
                Price = ad.Price,
                Description = ad.Description,
                Category = ad.Categories.Name,
                //CategoryId = ad.CategoryId,
            };

            return vm;
        }

    }


}
