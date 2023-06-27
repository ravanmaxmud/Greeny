using GrennyWebApplication.Areas.Client.ViewModels.Order;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(DataContext dbContext, IFileService fileService, IUserService userService, IOrderService orderService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet("checkout", Name = "client-order-checkout")]
        public async Task<IActionResult> CheckOut()
        {
            if (!_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-auth-login");
            }

            var model = new OrderProductsViewModel
            {
                Products = await _dbContext.BasketProducts
              .Where(p => p.Basket.UserId == _userService.CurrentUser.Id)
                .Select(p => new OrderProductsViewModel.ListItem
                {
                    Name = p.Plant.Title,
                    Price = p.Plant.Price,
                    Quantity = p.Quantity,
                    Total = p.Plant.Price * (decimal)p.Quantity!,
                }).ToListAsync(),

                Summary = new OrderProductsViewModel.SummaryTotal
                {
                    Total = await _dbContext.BasketProducts
                 .Where(pu => pu.Basket!.UserId == _userService.CurrentUser.Id)
                  .SumAsync(bp => bp.Plant!.Price * (decimal)bp.Quantity!)
                }
            };
            return View(model);
        }
    }
}
