using System.ComponentModel.DataAnnotations;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Plant
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal DiscountPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int InStock { get; set; }
        [Required]
        public List<int>? CategoryIds { get; set; }
        [Required]
        public List<int>? DiscountIds { get; set; }
        [Required]
        public List<int> BrandsIds { get; set; }

        [Required]
        public List<int>? TagIds { get; set; }
        



        public List<DiscountListViewModel>? Discounts { get; set; }
        public List<BrandListItemViewModel>? Brands { get; set; }
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
