using EMarketApp.Core.Application.Helpers;
using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels.Ads;
using EMarketApp.Core.Application.ViewModels.Users;
using EMarketApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EMarketApp.Core.Application.Services
{
    public class AdService : IAdsService
    {
        private readonly IAdsRepository _adRepository;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public AdService(IAdsRepository AdRepository, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _adRepository = AdRepository;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<List<AdViewModel>> GetAllViewModel()
        {
            var adsList = await _adRepository.GetAllWithIncludeAsync(new List<string> { "Categories" });

            return adsList.Where(ad => ad.UserId == userViewModel.Id).Select(ad => new AdViewModel
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

        public async Task<SaveAdViewModel> Add(SaveAdViewModel vm)
        {
            Ads ad = new();
            ad.UserId = userViewModel.Id;
            ad.Name = vm.Name;
            ad.ImagePathOne = "none";
            ad.ImagePathTwo = vm.ImagePathTwo;
            ad.ImagePathThree = vm.ImagePathThree;
            ad.ImagePathFour = vm.ImagePathFour;
            ad.Price = vm.Price;
            ad.Description = vm.Description;
            ad.CategoryId = vm.CategoryId;

            ad = await _adRepository.AddAsync(ad);

            SaveAdViewModel adVm = new();
            adVm.Id = ad.Id;
            adVm.Name = ad.Name;
            adVm.ImagePathOne = ad.ImagePathOne;
            adVm.ImagePathTwo = ad.ImagePathTwo;
            adVm.ImagePathThree = ad.ImagePathThree;
            adVm.ImagePathFour = ad.ImagePathFour;
            adVm.Price = ad.Price;
            adVm.Description = ad.Description;
            adVm.CategoryId = ad.CategoryId;

            return adVm;
        }

        public async Task Update(SaveAdViewModel vm)
        {
            Ads ad = await _adRepository.GetByIdAsync(vm.Id);
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
            var adViewModelList = adsList.Where(ad => ad.UserId != userViewModel.Id).Select(ad => new AdViewModel
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
                return FilterAdsByCategory(adViewModelList, vm.CategoryId);
            }

            if (!String.IsNullOrEmpty(vm.AdName))
            {
                adViewModelList = adViewModelList
                    .Where(ad => ad.Name.Contains(vm.AdName))
                    .ToList();
            }

            return adViewModelList;
        }

        public async Task<DetailsAdViewModel> GetAdDetailsByIdAsync(int id)
        {
            var adsList = await _adRepository.GetAllWithIncludeAsync(new List<string> { "Categories", "Users" });

            var adViewModelList = adsList.Where(ad => ad.Id == id && ad.UserId != userViewModel.Id).Select(ad => new DetailsAdViewModel
            {
                Name = ad.Name,
                ImagePathOne = ad.ImagePathOne,
                ImagePathTwo = ad.ImagePathTwo,
                ImagePathThree = ad.ImagePathThree,
                ImagePathFour = ad.ImagePathFour,
                Price = ad.Price,
                Description = ad.Description,
                Category = ad.Categories.Name,
                CreateAt = ad.Created,
                Author = ad.Users.Name,
                AuthorEmail = ad.Users.Email,
                AuthorPhone = ad.Users.Phone,


            }).First();

            return adViewModelList;
        }

        public List<AdViewModel> FilterAdsByCategory(List<AdViewModel> adViewModelList, string categoryId)
        {
            string[] categories = categoryId.Split(",");
            int[] categoriesIds = new int[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                categoriesIds[i] = Convert.ToInt32(categories[i]);
            }

            switch (categoriesIds.Length)
            {
                case 1: adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0]) .ToList(); break;
                case 2: adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0] || ad.CategoryId == categoriesIds[1]).ToList(); break;
                case 3:adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0] || ad.CategoryId == categoriesIds[1] || ad.CategoryId == categoriesIds[2]).ToList(); break;
                case 4: adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0] || ad.CategoryId == categoriesIds[1] || ad.CategoryId == categoriesIds[2] || ad.CategoryId == categoriesIds[3]).ToList(); break;
                case 5: adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0] || ad.CategoryId == categoriesIds[1] || ad.CategoryId == categoriesIds[2] || ad.CategoryId == categoriesIds[3] || ad.CategoryId == categoriesIds[3]).ToList(); break;
                default: adViewModelList = adViewModelList.Where(ad => ad.CategoryId == categoriesIds[0] || ad.CategoryId == categoriesIds[1] || ad.CategoryId == categoriesIds[2] || ad.CategoryId == categoriesIds[3] || ad.CategoryId == categoriesIds[3] || ad.CategoryId == categoriesIds[^1]).ToList(); break;
            }

            return adViewModelList;
        }

    }


}
