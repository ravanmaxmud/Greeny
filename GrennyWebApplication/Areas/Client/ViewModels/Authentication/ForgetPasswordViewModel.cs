using System.ComponentModel.DataAnnotations;

namespace GrennyWebApplication.Areas.Client.ViewModels.Authentication
{
    public class ForgetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
