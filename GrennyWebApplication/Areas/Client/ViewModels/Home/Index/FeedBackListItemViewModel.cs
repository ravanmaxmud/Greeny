namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class FeedBackListItemViewModel
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Context { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }
        public FeedBackListItemViewModel(int id, string fullName, string context, string role, string imageUrl)
        {
            Id = id;
            FullName = fullName;
            Context = context;
            Role = role;
            ImageUrl = imageUrl;
        }
      


    }
}
