using GrennyWebApplication.Areas.Client.ViewComponents;
using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shopPage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ShopPageController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("index", Name = "client-shop-index")]
        public async Task<IActionResult> Index(string searchBy, string search, int? categoryId = null, int? tagId = null, int? brandId = null)
        {
            ViewBag.SearchBy = searchBy;
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.TagId = tagId;

            var model = new IndexViewModel
            {
                Categories = await _dataContext.Categories
                     .Select(c => new CategoryViewModel(c.Id, c.Title,
                     c.Catagories.Select(s => new SubCategoryViewModel(s.Id, s.Title)).ToList())).ToListAsync(),

                Tags = await _dataContext.Tags.Select(t => new TagViewModel(t.Id, t.TagName)).ToListAsync() ,

                Brands = await _dataContext.Brands.Select(b=> new BrandViewModel(b.Id,b.Name)).ToListAsync(),
            };

            return View(model);
        }

        [HttpGet("sort", Name = "client-shop-sort")]
        public async Task<IActionResult> Sort(string? searchBy, string? search, [FromQuery] int? sort = null, [FromQuery] int? categoryId = null,
           int? minPrice = null,
           int? maxPrice = null,
           [FromQuery] int? tagId = null, [FromQuery] int? brandId=null)
        {

            return ViewComponent(nameof(ShopPageProduct), new { searchBy = searchBy, search = search, sort = sort, categoryId = categoryId, minPrice = minPrice, maxPrice = maxPrice, tagId = tagId, brandId = brandId });
        }
    }
}
