namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class BlogListItemViewModel
    {
        public BlogListItemViewModel(int id, string title, string description, string fileUrl, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            FileUrl = fileUrl;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
