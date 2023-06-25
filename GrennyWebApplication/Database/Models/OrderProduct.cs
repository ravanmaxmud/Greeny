using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Database.Models.Common;
using GrennyWebApplication.Database.Models.Enums;

namespace GrennyWebApplication.Database.Models
{
    public class OrderProduct : BaseEntity<int>, IAuditable
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
