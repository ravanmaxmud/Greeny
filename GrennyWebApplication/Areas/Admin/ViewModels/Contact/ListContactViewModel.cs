namespace GrennyWebApplication.Areas.Admin.ViewModels.Contact
{
    public class ListContactViewModel
    {

        public string FirstName { get; set; }

        public string Subject { get; set; }


        public string Email { get; set; }

        public string Message { get; set; }
        public ListContactViewModel(string firstName, string subject, string email, string message)
        {
            FirstName = firstName;
            Subject = subject;
            Email = email;
            Message = message;
        }
       
    }
}
