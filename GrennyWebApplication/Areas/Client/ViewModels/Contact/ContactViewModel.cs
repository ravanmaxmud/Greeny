namespace GrennyWebApplication.Areas.Client.ViewModels.Contact
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<CityViewModel>? Cities { get; set; }
    }
}
