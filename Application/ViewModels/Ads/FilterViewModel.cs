using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketApp.Core.Application.ViewModels.Ads
{
    public class FilterViewModel
    {
        [Required(ErrorMessage = "You must enter the name of the ad")]
        public string? AdName { get; set; }

        [Required(ErrorMessage = "You must select at least one category")]
        public string CategoryId { get; set; }
    }
}
