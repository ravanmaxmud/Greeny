using GrennyWebApplication.Areas.Admin.ViewModels.Plant;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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

            model.Brands = await _dataContext.Brands.Select(b => new BrandListItemViewModel(b.Id, b.Name)).ToListAsync();

            model.Discounts = await _dataContext.Disconts.Select(d => new DiscountListViewModel(d.Id, d.Title)).ToListAsync();


            return model;
        }

    }
}
