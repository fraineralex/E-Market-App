using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketApp.Core.Application.ViewModels
{
    public class FilterViewModel
    {
        public String? AdName { get; set; }
        public int? CategoryId { get; set; }
        public int? AdId { get; set; }
    }
}
