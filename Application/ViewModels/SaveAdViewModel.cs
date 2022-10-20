using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EMarketApp.Core.Application.ViewModels
{
    public class SaveAdViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "You must enter the name of the ad")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string? ImagePathFour { get; set; }
        [Required(ErrorMessage = "You must enter the price of the ad")]
        [DataType(DataType.Currency)]
        public double? Price { get; set; }
        [Required(ErrorMessage = "You must enter the description of the ad")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "You must enter the category of the ad")]
        public int CategoryId { get; set; }

        public List<CategoryViewModel>? CategoriesList { get; set; }

        public IFormFile File1 { get; set; }
        public IFormFile File2 { get; set; }
        public IFormFile File3 { get; set; }
        public IFormFile File4 { get; set; }

    }

}
