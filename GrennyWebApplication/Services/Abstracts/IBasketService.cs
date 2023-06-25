using GrennyWebApplication.Areas.Client.ViewModels.Authentication;
using GrennyWebApplication.Areas.Client.ViewModels.Basket;
using GrennyWebApplication.Database.Models;

namespace GrennyWebApplication.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant);
    }
}
