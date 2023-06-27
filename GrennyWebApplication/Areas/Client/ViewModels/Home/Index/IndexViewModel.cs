namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<SliderLIstItemViewModel> Sliders { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        public List<GlobalOfferViewModel> GlobalOffers { get; set; }
        public List<FeedBackListItemViewModel> FeedBacks { get; set; }
        public List<PlantViewModel> Plants { get; set; }
        public List<BrandViewModel>? Brands { get; set; }
        public List<BlogListItemViewModel>? Blogs { get; set; }
        public List<BlogCategoryViewModel>? BlogCategories { get; set; }
        public List<BlogTagViewModel>? BlogTags { get; set; }


    }
}
