
using EMarketApp.Core.Domain.Common;

namespace EMarketApp.Core.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }

        //Navigation property
        public ICollection<Ads> Ads { get; set; }
        public ICollection<Categories> Categories { get; set; }
    }
}
