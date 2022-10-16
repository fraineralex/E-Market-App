
using EMarketApp.Core.Domain.Common;

namespace EMarketApp.Core.Domain.Entities
{
    public class Ads : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string? ImagePathFour { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }

        // Foreign key fields

        public int CategoryId { get; set; }

        //Navigation property
        public Categories? Categories { get; set; }
    }
}
