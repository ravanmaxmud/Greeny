namespace GrennyWebApplication.Areas.Client.ViewModels.Contact
{
    public class CityViewModel
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
        public CityViewModel(string name, string address, string ımgUrl)
        {
            Name = name;
            Address = address;
            ImgUrl = ımgUrl;
        }

    }
}
