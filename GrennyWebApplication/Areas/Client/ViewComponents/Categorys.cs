using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Categorys")]
    public class Categorys : ViewComponent
    {

        private readonly DataContext _dataContext;

        public Categorys(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                await _dataContext.Categories
                .Where(c => c.ParentId == null).Select(c => new CategoryViewModel(c.Id, c.Title,
                c.Catagories.Where(x => x.ParentId == c.Id).Select(s => new SubCategoryViewModel(s.Id, s.Title)).ToList()))
                .ToListAsync();


            return View(model);
        }
    }
}
