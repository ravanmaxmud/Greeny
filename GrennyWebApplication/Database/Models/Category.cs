using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class Category : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<PlantCatagory> ProductCatagories { get; set; }
        public List<Category> Catagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
