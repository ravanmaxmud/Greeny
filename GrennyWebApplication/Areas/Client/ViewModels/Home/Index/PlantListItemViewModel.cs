namespace GrennyWebApplication.Areas.Client.ViewModels.Home.Index
{
    public class PlantListItemViewModel
    {
        public PlantListItemViewModel(int id, string title,  decimal price, string imageUrl)
        {
            Id = id;
            Title = title;
            Price = price;
            ImageUrl = imageUrl;
          
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        
    }
}
