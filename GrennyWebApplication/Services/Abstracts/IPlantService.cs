using GrennyWebApplication.Areas.Admin.ViewModels.Plant;
using GrennyWebApplication.Database.Models;
namespace GrennyWebApplication.Services.Abstracts
{
    public interface IPlantService
    {
        public void FindPers(AddViewModel model, Plant product);

        public Task<AddViewModel> GetViewForModel(AddViewModel model);
        //public  Task<UpdateViewModel> GetViewForModel(UpdateViewModel model);
    }
}
