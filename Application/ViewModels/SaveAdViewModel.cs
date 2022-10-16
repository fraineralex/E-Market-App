using System.ComponentModel.DataAnnotations;

namespace EMarketApp.Core.Application.ViewModels
{
    public class SaveAdViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "You must enter the name of the ad")]
        public string? Name { get; set; }
        //[Required(ErrorMessage = "You must enter the image of the ad")]
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string? ImagePathFour { get; set; }

        public double? Price { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "You must enter the category of the ad")]
        public int CategoryId { get; set; }

        public List<CategoryViewModel>? CategoriesList { get; set; }

    }

}
