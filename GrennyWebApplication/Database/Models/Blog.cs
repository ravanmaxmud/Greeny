using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class Blog : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
        public List<BlogFile>? BlogFile { get; set; }
        public List<BlogAndBlogTag>? BlogTags { get; set; }
        public List<BlogAndBlogCategory>? BlogCategory { get; set; }





    }
}
