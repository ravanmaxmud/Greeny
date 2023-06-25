using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class PlantBrand :BaseEntity<int>, IAuditable
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
