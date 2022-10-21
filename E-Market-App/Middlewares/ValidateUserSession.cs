using EMarketApp.Core.Application.Helpers;
using EMarketApp.Core.Application.ViewModels.Users;

namespace E_Market_App.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if(userViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
