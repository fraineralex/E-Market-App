
using System.ComponentModel.DataAnnotations;

namespace EMarketApp.Core.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter your username")]
        [DataType(DataType.Text)]
        public string? Username { get; set; }
        [Required(ErrorMessage = "You must enter your password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
