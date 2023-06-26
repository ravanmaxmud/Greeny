using GrennyWebApplication.Areas.Client.ViewComponents;
using GrennyWebApplication.Areas.Client.ViewModels.Basket;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("cartPage")]
    public class CartPageController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IBasketService _basketService;

        public CartPageController(DataContext dataContext, IUserService userService = null, IFileService fileService = null, IBasketService basketService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
            _basketService = basketService;
        }
        [HttpGet("index", Name = "client-cart-index")]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpGet("delete/{productId}", Name = "client-cart-delete")]
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
            return ViewComponent(nameof(CartPage), productCookieViewModel);
        }
        [HttpGet("update", Name = "client-cart-update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return ViewComponent(nameof(MiniBasket));
        }
    }
}
