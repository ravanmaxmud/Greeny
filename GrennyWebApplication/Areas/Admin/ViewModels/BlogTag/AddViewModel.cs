namespace GrennyWebApplication.Areas.Admin.ViewModels.BlogTag
{
    public class AddViewModel
    {
        public string Tagname { get; set; }


        public AddViewModel()
        {   

        }

        public AddViewModel(string tagName)
        {
            Tagname = tagName;

        }
    }
}
