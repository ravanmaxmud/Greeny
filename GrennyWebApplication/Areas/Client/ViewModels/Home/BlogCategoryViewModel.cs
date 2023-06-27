namespace GrennyWebApplication.Areas.Client.ViewModels.Home
{
    public class BlogCategoryViewModel
    {
        public BlogCategoryViewModel(int id, string name, int blogCount)
        {
            Id = id;
            Name = name;
            BlogCount = blogCount;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BlogCount { get; set; }
    }
}
