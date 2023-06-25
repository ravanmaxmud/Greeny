namespace GrennyWebApplication.Areas.Admin.ViewModels.Plant
{
    public class BrandListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public BrandListItemViewModel(int ıd, string title)
        {
            Id = ıd;
            Title = title;
        }
    }
}
