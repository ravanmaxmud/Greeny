using GrennyWebApplication.Areas.Client.ViewModels.Authentication;
using GrennyWebApplication.Database.Models;

namespace GrennyWebApplication.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user); 

    }
}
