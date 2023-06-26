namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class GlobalOfferViewModel
    {
        public string Title { get; set; }
        public DateTime OfferTime { get; set; }
        public GlobalOfferViewModel(string title, DateTime offerTime)
        {
            Title = title;
            OfferTime = offerTime;
        }
    }
}
