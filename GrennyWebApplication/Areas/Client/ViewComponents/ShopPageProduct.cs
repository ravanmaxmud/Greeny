using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageProduct")]
    public class ShopPageProduct : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageProduct(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? searchBy = null,
            string? search = null, [FromQuery] int? sort = null, [FromQuery] int? categoryId = null,
            int? minPrice = null, int? maxPrice = null,
            [FromQuery] int? tagId = null, [FromQuery] int? brandId = null)
        {
            var productsQuery = _dataContext.Plants.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Title.StartsWith(search) || search == null);
            }
            else if (sort is not null)
            {
                switch (sort)
                {
                    case 1:
                        productsQuery = productsQuery.OrderBy(p => p.Title);
                        break;

                    case 2:
                        productsQuery = productsQuery.OrderByDescending(p => p.Title);
                        break;
                    case 3:
                        productsQuery = productsQuery.OrderByDescending(p => p.CreatedAt);
                        break;
                    case 4:
                        productsQuery = productsQuery.OrderBy(p => p.Price);
                        break;
                    case 5:
                        productsQuery = productsQuery.OrderByDescending(p => p.Price);
                        break;
                }
            }
            else if (categoryId is not null || tagId is not null || brandId is not null)
            {
                productsQuery = productsQuery.Include(p => p.PlantCatagories).Include(p => p.PlantTags).Include(p => p.PlantBrands)
                    .Where(p => categoryId == null || p.PlantCatagories!.Any(pc => pc.CategoryId == categoryId))
                    .Where(p => tagId == null || p.PlantTags!.Any(pt => pt.TagId == tagId))
                    .Where(p => brandId == null || p.PlantBrands!.Any(pb=> pb.BrandId == brandId));
            }
            else if (minPrice is not null && maxPrice is not null)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }


            var model = await productsQuery.Include(p => p.PlantImages)
                .Select(p => new PlantViewModel(p.Id, p.Title, p.Price, p.DiscountPrice, p.Content,
                p.PlantImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.PlantImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Plant) : String.Empty
                )).ToListAsync();

            //ViewBag.CurrentPage = page;
            //ViewBag.TotalPage = Math.Ceiling((decimal)_dataContext.Products.Count() / 4);

            return View(model);
        }
    }
}
