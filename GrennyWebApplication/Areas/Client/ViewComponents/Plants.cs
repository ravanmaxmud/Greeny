using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.ViewComponents
{

    [ViewComponent(Name = "Plants")]
    public class Plants : ViewComponent
    {

        private readonly DataContext _dataContext;

        public Plants(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? slide = null)
        {
            var productsQuery =  _dataContext.Plants.AsQueryable();

            if (slide == "New")
            {
                productsQuery = productsQuery.OrderBy(P => P.CreatedAt);
            }
            else if (slide == "Discount") 
            {
                productsQuery = productsQuery.Where(P => P.DiscountPrice != null);
            }

            var model =await productsQuery.Select(p => new PlantViewModel(p.Id, p.Title, p.Price, p.DiscountPrice, p.Content)).ToListAsync();



            return View(model);
        }
    }
}
