using GrennyWebApplication.Areas.Client.ViewModels.Authentication;
using GrennyWebApplication.Areas.Client.ViewModels.Basket;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Contracts.Identity;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Exceptions;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace GrennyWebApplication.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }


        public async Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant)
        {
            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();
            }
             
            return new List<ProductCookieViewModel>();
       
            //Add product to database if user is authenticated
            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                    .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.PlantId == plant.Id);
                if (basketProduct is not null)
                {
                    basketProduct.Quantity++;
                }
                else
                {
                    var basket = await _dataContext.Baskets.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = 1,
                        BasketId = basket.Id,
                        PlantId = plant.Id,
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }

                await _dataContext.SaveChangesAsync();
            }

        }
    }
}
