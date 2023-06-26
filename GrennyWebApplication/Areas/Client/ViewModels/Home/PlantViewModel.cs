namespace GrennyWebApplication.Areas.Client.ViewModels.Home
{
    public class PlantViewModel
    {
        public PlantViewModel(int id, string name, decimal price, decimal? discountPrice, string content, string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            DiscountPrice = discountPrice;
            Content = content;
            ImageUrl = imageUrl;
        }
        public PlantViewModel()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
