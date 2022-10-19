﻿using EMarketApp.Core.Application.Helpers;
using EMarketApp.Core.Application.Interfaces.Repositories;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
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

        public async Task Add(SaveAdViewModel vm)
        {
            Ads ad = new();
            ad.UserId = userViewModel.Id;
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
            var adViewModelList = adsList.Where(ad => ad.UserId == userViewModel.Id).Select(ad => new AdViewModel
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
            var adsList = await _adRepository.GetAllWithIncludeAsync(new List<string> { "Categories" });

            var adViewModelList = adsList.Where(ad => ad.Id == id && ad.UserId == userViewModel.Id).Select(ad => new AdViewModel
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
                CreateAt = ad.Created,

            }).First();

            return adViewModelList;
        }

    }


}
