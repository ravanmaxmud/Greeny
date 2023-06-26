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
            if (!_userService.IsAuthenticated)
            {
                return BadRequest("Login");
            }
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
        [HttpPost("basket-delete/{productId}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var productCookieViewModel = new List<ProductCookieViewModel>();

                var basketProduct = await _dataContext.BasketProducts
                   .Include(b => b.Basket).FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.Id == productId);

                if (basketProduct is null)
                {
                    return NotFound();
                }
                _dataContext.BasketProducts.Remove(basketProduct);
            

            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(MiniBasket), productCookieViewModel);
        }
    }
}
