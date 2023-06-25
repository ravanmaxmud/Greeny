using GrennyWebApplication.Areas.Admin.ViewModels.Plant;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Services.Concretes
{
    public class PlantService : IPlantService
    {
        private readonly DataContext _dataContext;

        public PlantService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void FindPers(AddViewModel model, Plant product)
        {

        }

        public async Task<AddViewModel> GetViewForModel(AddViewModel model)
        {

            model.Categories = await _dataContext.Categories
               .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
            .ToListAsync();

            model.Tags = await _dataContext.Tags
             .Select(c => new TagListItemViewModel(c.Id, c.TagName))
             .ToListAsync();


            return model;
        }

    }
}
