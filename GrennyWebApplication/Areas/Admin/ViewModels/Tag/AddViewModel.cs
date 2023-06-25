namespace GrennyWebApplication.Areas.Admin.ViewModels.Tag
{
    public class AddViewModel
    {

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public AddViewModel()
        {

        }
        public AddViewModel(string name, DateTime createdDate, DateTime updatedDate)
        {
            Name = name;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }

       
    }
}
