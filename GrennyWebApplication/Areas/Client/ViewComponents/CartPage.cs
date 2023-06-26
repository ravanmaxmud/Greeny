using GrennyWebApplication.Areas.Client.ViewModels.Basket;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.ViewComponents
{
    
    [ViewComponent(Name = "CartPage")]
    public class CartPage : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CartPage(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            if (_userService.IsAuthenticated)
            {

                var model = await _dataContext.BasketProducts.Where(p => p.Basket.UserId == _userService.CurrentUser.Id)
                        .Select(p =>
                        new ProductCookieViewModel(p.Id, p.Plant.Title, p.Plant.PlantImages.Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.Plant.PlantImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Plant)
                    : String.Empty,
                    p.Quantity, p.Plant.Price,
                    p.Plant.Price * p.Quantity))
                        .ToListAsync();

                return View(model);


            }

            return View(viewModels);

        }
    }
}
