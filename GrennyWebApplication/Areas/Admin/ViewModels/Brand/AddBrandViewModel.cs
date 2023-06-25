namespace GrennyWebApplication.Areas.Admin.ViewModels.Brand
{
    public class AddBrandViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
