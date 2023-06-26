namespace GrennyWebApplication.Areas.Admin.ViewModels.City
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
