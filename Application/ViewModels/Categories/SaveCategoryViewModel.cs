using System.ComponentModel.DataAnnotations;

namespace EMarketApp.Core.Application.ViewModels.Categories
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of the category")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "You must enter the description of the category")]
        public string? Description { get; set; }
    }
}
