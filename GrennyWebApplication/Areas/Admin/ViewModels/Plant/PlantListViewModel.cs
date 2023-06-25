using static BackEndFinalProject.Areas.Client.ViewModels.Home.Modal.ModalViewModel;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Plant
{
    public class PlantListViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int InStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public List<BrandViewModel> Brands { get; set; }
        public List<DiscountViewModel> Discounts { get; set; }
        public List<TagViewModel> Tags { get; set; }
       
        public PlantListViewModel(int ıd, string name, string description, decimal price, 
            decimal? discountPrice, int ınStock, DateTime createdAt, List<CategoryViewModeL> categories,
            List<BrandViewModel> brands, List<DiscountViewModel> discounts, List<TagViewModel> tags)
        {
            Id = ıd;
            Name = name;
            Description = description;
            Price = price;
            DiscountPrice = discountPrice;
            InStock = ınStock;
            CreatedAt = createdAt;
            Categories = categories;
            Brands = brands;
            Discounts = discounts;
            Tags = tags;
        }

        public class BrandViewModel
        {
            public BrandViewModel(string title)
            {
                Title = title;

            }

            public string Title { get; set; }



        }

        public class DiscountViewModel
        {

            public string Title { get; set; }
            public int DiscontPers { get; set; }
            public DateTime DiscountTime { get; set; }
            public DiscountViewModel(string title, int discontPers, DateTime discountTime)
            {
                Title = title;
                DiscontPers = discontPers;
                DiscountTime = discountTime;
            }

        }

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }

      
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
    }
}
