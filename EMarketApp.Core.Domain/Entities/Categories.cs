
using EMarketApp.Core.Domain.Common;

namespace EMarketApp.Core.Domain.Entities
{
    public class Categories : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation property
        public ICollection<Ads> Ads { get; set; }
    }
}
