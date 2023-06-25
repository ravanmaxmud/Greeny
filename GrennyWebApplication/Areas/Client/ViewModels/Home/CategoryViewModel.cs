namespace GrennyWebApplication.Areas.Client.ViewModels.Home
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string title, List<SubCategoryViewModel> subCategories)
        {
            Id = id;
            Title = title;
            SubCategories = subCategories;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<SubCategoryViewModel> SubCategories { get; set; }



    }
    public class SubCategoryViewModel
    {
        public SubCategoryViewModel(int id, string subName)
        {
            Id = id;
            SubName = subName;
        }

        public int Id { get; set; }
        public string SubName { get; set; }
    }
}
