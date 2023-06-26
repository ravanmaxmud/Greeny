using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.ViewComponents
{

    [ViewComponent(Name = "Plants")]
    public class Plants : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Plants(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
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

            var model = await productsQuery.Include(p => p.PlantImages)
                .Select(p => new PlantViewModel(p.Id, p.Title, p.Price, p.DiscountPrice, p.Content,
                p.PlantImages.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.PlantImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Plant) : String.Empty
                )).ToListAsync();



            return View(model);
        }
    }
}
