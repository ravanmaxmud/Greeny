namespace GrennyWebApplication.Areas.Admin.ViewModels.City
{
    public class ListViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
        public ListViewModel(int ıd, string name, string address, DateTime createdAt, DateTime updatedAt, string ımageUrl)
        {
            Id = ıd;
            Name = name;
            Address = address;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ImageUrl = ımageUrl;
        }
    }
}
