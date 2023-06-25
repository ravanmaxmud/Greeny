namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class PaymmentLIstItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string ImageUrl { get; set; }
        public PaymmentLIstItemViewModel(int id, string title, string context, string imageUrl)
        {
            Id = id;
            Title = title;
            Context = context;
            ImageUrl = imageUrl;
        }
    }
}
