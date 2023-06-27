namespace GrennyWebApplication.Areas.Client.ViewModels.About
{
    public class AboutListItemViewModel
    {
       
        public string Description { get; set; }
        public AboutListItemViewModel(string description)
        {
            Description = description;
        }
    }
}
