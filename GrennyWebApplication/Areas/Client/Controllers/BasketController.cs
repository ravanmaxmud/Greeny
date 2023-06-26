using BackEndFinalProject.Areas.Client.ViewModels.Home.Modal;
using GrennyWebApplication.Areas.Client.ViewComponents;
using GrennyWebApplication.Areas.Client.ViewModels.Basket;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }

        [HttpPost("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id, ProductCookieViewModel model)
        {
            var plant = await _dataContext.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p=> p.Id == id);

            if (plant is null)
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(plant);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(MiniBasket), productCookiViewModel);
            }
            return ViewComponent(nameof(MiniBasket), plant);
        }
    }
}
