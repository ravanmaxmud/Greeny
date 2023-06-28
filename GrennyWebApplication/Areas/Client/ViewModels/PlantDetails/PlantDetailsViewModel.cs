using GrennyWebApplication.Areas.Client.ViewModels.Comment;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;


namespace GrennyWebApplication.Areas.Client.ViewModels.PlantDetails
{
    public class PlantDetailsViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? InStock { get; set; }

        public string? Name { get; set; }
        public string? Context { get; set; }
        public string? Email { get; set; }
        public List<CommentViewModel>? Comment { get; set; }
        public List<ImageViewModeL>? Images { get; set; }
        public List<DiscountList>? Discounts { get; set; }
        public List<PlantListItemViewModel>? Products { get; set; }





        public class ImageViewModeL
        {
            public ImageViewModeL(string imageUrl)
            {
                ImageUrl = imageUrl;
            }
            public string ImageUrl { get; set; }
        }
        public class DiscountList
        {
            public DiscountList(int ıd, int percentage)
            {
                Id = ıd;
                Percentage = percentage;
            }

            public int Id { get; set; }
            public int Percentage { get; set; }
        }

    }
}
