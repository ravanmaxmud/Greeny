using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class BlogCategory : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }
        public List<BlogAndBlogCategory> BlogCatagories { get; set; }
        public List<BlogCategory> Catagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
