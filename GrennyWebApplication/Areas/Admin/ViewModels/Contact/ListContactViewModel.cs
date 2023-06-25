namespace GrennyWebApplication.Areas.Admin.ViewModels.Contact
{
    public class ListContactViewModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
        public ListContactViewModel(string firstName, string lastName, string phone, string email, string message)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Message = message;
        }
    }
}
