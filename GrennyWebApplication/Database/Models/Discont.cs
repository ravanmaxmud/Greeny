using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class Discont : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public int DiscontPers { get; set; }
        public DateTime DiscountTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PlantDiscont> ProductDisconts { get; set; }
    }
}
