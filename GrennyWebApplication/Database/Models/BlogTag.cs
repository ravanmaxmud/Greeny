using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class BlogTag : BaseEntity<int>, IAuditable
    {
        public string TagName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        List<BlogAndBlogTag> Tags { get; set; }


    }
}
