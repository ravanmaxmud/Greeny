namespace GrennyWebApplication.Areas.Client.ViewModels.Home
{
    public class BlogTagViewModel
    {
        public BlogTagViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
