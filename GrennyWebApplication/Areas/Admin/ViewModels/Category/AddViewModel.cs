namespace GrennyWebApplication.Areas.Admin.ViewModels.Category
{
    public class AddViewModel
    {

        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public AddViewModel()
        {

        }
        public AddViewModel(int parentId, string name, DateTime createdDate, DateTime updatedDate)
        {
            ParentId = parentId;
            Name = name;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }
}
