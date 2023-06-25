

namespace GrennyWebApplication.Areas.Admin.ViewModels.Blog
{
    public class BlogListViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }
       

        public BlogListViewModel(int id, string name, string description, DateTime createdAt, List<CategoryViewModeL> categories, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            Categories = categories;
            Tags = tags;
        }



        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }




    }
}
