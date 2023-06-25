using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class PlantCatagory : BaseEntity<int>, IAuditable
    {
        public int PLantId { get; set; }
        public Plant Plant { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
