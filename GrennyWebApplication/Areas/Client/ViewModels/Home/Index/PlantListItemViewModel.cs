namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class PlantListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public List<CategoriesList> Categories { get; set; }
        public string ImageUrl { get; set; }

        public PlantListItemViewModel(int ıd, string title, decimal price, decimal? discountPrice, List<CategoriesList> categories, string ımageUrl)
        {
            Id = ıd;
            Title = title;
            Price = price;
            DiscountPrice = discountPrice;
            Categories = categories;
            ImageUrl = ımageUrl;
        }
        public class CategoriesList
        {

            public string Title { get; set; }
            public CategoriesList(string title)
            {
                Title = title;
            }
        }



    }
}
