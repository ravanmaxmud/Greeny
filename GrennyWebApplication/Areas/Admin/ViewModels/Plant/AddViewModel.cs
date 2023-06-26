
using System.ComponentModel.DataAnnotations;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Plant
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int InStock { get; set; }
        
        public List<int>? CategoryIds { get; set; }

        
        public List<int>? TagIds { get; set; }
        
        public List<int>? BrandIds { get; set; }
        
        public List<int>? DicountIds { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public List<BrandListItemViewModel>? Brands { get; set; }
        public List<DiscountListViewModel>? Discounts { get; set; }
        public List<CatagoryListItemViewModel>? Categories { get; set; }
      
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
