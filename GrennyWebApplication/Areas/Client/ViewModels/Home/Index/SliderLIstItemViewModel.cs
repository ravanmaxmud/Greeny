namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class SliderLIstItemViewModel
    {
        public string Title { get; set; }
        public string OfferContext { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ImageUrl { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public SliderLIstItemViewModel(string title,string offerContext, string content, string buttonName, string imageUrl, string buttonRedirectUrl, int order)
        {
            Title = title;
            OfferContext = offerContext;
            Content = content;
            ButtonName = buttonName;
            ImageUrl = imageUrl;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
        }
    }
}
