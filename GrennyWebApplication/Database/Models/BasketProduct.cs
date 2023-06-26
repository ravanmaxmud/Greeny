using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class BasketProduct : BaseEntity<int>, IAuditable
    {
        public int BasketId { get; set; }
        public Basket? Basket { get; set; }

        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public int? Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
