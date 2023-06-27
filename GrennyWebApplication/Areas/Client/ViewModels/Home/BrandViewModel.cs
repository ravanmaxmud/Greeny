namespace GrennyWebApplication.Areas.Client.ViewModels.Home
{
    public class BrandViewModel
    {
        public BrandViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
