using System.ComponentModel.DataAnnotations;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Blog
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public List<int>? CategoryIds { get; set; }
        [Required]
        public List<int>? TagIds { get; set; }
        [Required]
        public string Description { get; set; }
       



        public List<CatagoryListItemViewModel>? Categories { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
