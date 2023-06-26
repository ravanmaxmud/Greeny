using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Database;
using GrennyWebApplication.Areas.Admin.ViewModels.Plant;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class PlantController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<PlantController> _logger;
        private readonly IPlantService _productService;



        public PlantController(DataContext dataContext, ILogger<PlantController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;

        }
        #region List

        [HttpGet("list", Name = "admin-plant-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Plants.Select(p => new PlantListViewModel(
                p.Id,
                p.Title,
                p.Content,
                p.Price,
                p.DiscountPrice,
                p.InStock,
                p.CreatedAt,
                p.PlantCatagories.Select(pc => pc.Category).Select(c => new PlantListViewModel.CategoryViewModeL(c.Title,c.Parent.Title)).ToList(),
                p.PlantBrands.Select(pb => pb.Brand).Select(b => new PlantListViewModel.BrandViewModel(b.Name)).ToList(),
                p.PlantDisconts.Select(pd => pd.Discont).Select(d => new PlantListViewModel.DiscountViewModel(d.Title, d.DiscontPers, d.DiscountTime)).ToList(),
                p.PlantTags.Select(ps => ps.Tag).Select(s => new PlantListViewModel.TagViewModel(s.TagName)).ToList()
                )).ToListAsync();


            return View(model);
        }

        #endregion

        #region Add
        [HttpGet("add", Name = "admin-plant-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.Categories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                    .ToListAsync(),

                Tags = await _dataContext.Tags.Select(t => new TagListItemViewModel(t.Id, t.TagName)).ToListAsync(),
                Brands = await _dataContext.Brands.Select(b => new BrandListItemViewModel(b.Id, b.Name)).ToListAsync(),
                Discounts = await _dataContext.Disconts.Select(d => new DiscountListViewModel(d.Id, d.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-plant-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await _productService.GetViewForModel(model);
                return View(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }
            foreach (var brandId in model.BrandIds)
            {
                if (!await _dataContext.Brands.AnyAsync(c => c.Id == brandId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({brandId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }
            if (model.Discounts != null)
            {
                foreach (var discountId in model.DicountIds)
                {
                    if (!await _dataContext.Disconts.AnyAsync(c => c.Id == discountId))
                    {
                        ModelState.AddModelError(string.Empty, "Something went wrong");
                        _logger.LogWarning($"Brand with id({discountId}) not found in db ");
                        model = await _productService.GetViewForModel(model);
                        return View(model);
                    }

                }

            }


            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }


            var product = new Plant
            {
                Title = model.Name,
                Content = model.Description,
                Price = model.Price,
                InStock = model.InStock,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            if (model.DicountIds != null)
            {
                var discounts = await _dataContext.PlantDisconts.Select(d => d.DiscontId).ToListAsync();
                var items = discounts.Intersect(model.DicountIds).ToList();


                foreach (var item in items)
                {
                    var discasount = await _dataContext.Disconts.FirstOrDefaultAsync(pd => pd.Id == item);
                    if (discasount != null)
                    {
                        decimal discountPercentage = discasount.DiscontPers;
                        var sum = (product.Price * discasount.DiscontPers) / 100;
                        product.DiscountPrice = product.Price - sum;
                    }
                    else
                    {
                        product.DiscountPrice = null;
                    }

                }

            }



            await _dataContext.Plants.AddAsync(product);

            foreach (var catagoryId in model.CategoryIds)
            {
                var productCatagory = new PlantCatagory
                {
                    CategoryId = catagoryId,
                    Plant = product,
                };

                await _dataContext.PlantCatagories.AddAsync(productCatagory);
            }


            foreach (var tagId in model.TagIds)
            {
                var productTag = new PlantTag
                {
                    TagId = tagId,
                    Plant = product,
                };

                await _dataContext.PlantTags.AddAsync(productTag);
            }
            foreach (var brandId in model.BrandIds)
            {
                var productBrand = new PlantBrand
                {
                    BrandId = brandId,
                    Plant = product,
                };

                await _dataContext.PlantBrands.AddAsync(productBrand);
            }

            if (model.DicountIds != null)
            {

                foreach (var discountId in model.DicountIds)
                {
                    var productDiscount = new PlantDiscont
                    {
                        DiscontId = discountId,
                        Plant = product,
                    };

                    await _dataContext.PlantDisconts.AddAsync(productDiscount);
                }

            }



            await _dataContext.Plants.AddAsync(product);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-list");







        }

        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-plant-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Plants
                .Include(c => c.PlantCatagories)
                .Include(c => c.PlantTags)
                .Include(s => s.PlantBrands)
                .Include(d => d.PlantDisconts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Content,
                InStock = product.InStock,
                Categories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.PlantCatagories.Select(pc => pc.CategoryId).ToList(),
                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.TagName)).ToListAsync(),
                TagIds = product.PlantTags.Select(pc => pc.TagId).ToList(),
                Brands = await _dataContext.Brands.Select(c => new BrandListItemViewModel(c.Id, c.Name)).ToListAsync(),
                BrandsIds = product.PlantBrands.Select(pc => pc.BrandId).ToList(),
                Discounts = await _dataContext.Disconts.Select(d => new DiscountListViewModel(d.Id, d.Title)).ToListAsync(),
                DiscountIds = product.PlantDisconts.Select(d => d.DiscontId).ToList()

            };

            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-plant-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var product = await _dataContext.Plants
                    .Include(c => c.PlantCatagories)
                    .Include(c => c.PlantTags)
                    .Include(s => s.PlantBrands)
                    .Include(d => d.PlantDisconts)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (model.DiscountIds != null)
            {
                foreach (var discountId in model.DiscountIds)
                {
                    if (!await _dataContext.Disconts.AnyAsync(c => c.Id == discountId))
                    {
                        ModelState.AddModelError(string.Empty, "Something went wrong");
                        _logger.LogWarning($"Brand with id({discountId}) not found in db ");
                        return GetView(model);
                    }

                }

            }

            foreach (var brandId in model.BrandsIds)
            {
                if (!await _dataContext.Brands.AnyAsync(c => c.Id == brandId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({brandId}) not found in db ");

                    return GetView(model);
                }

            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }


            product.Title = model.Title;
            product.Content = model.Description;
            product.Price = model.Price;
            product.InStock = model.InStock;

            if (model.DiscountIds != null)
            {
                var discounts = _dataContext.PlantDisconts.Select(d => d.DiscontId).ToList();
                var items = discounts.Intersect(model.DiscountIds).ToList();

                foreach (var item in items)
                {
                    var discasount = _dataContext.Disconts.FirstOrDefault(pd => pd.Id == item);
                    if (discasount != null)
                    {
                        decimal discountPercentage = discasount.DiscontPers;
                        var sum = (product.Price * discasount.DiscontPers) / 100;
                        product.DiscountPrice = product.Price - sum;
                    }
                    else
                    {
                        product.DiscountPrice = null;
                    }
                }
            }
            else
            {

                product.DiscountPrice = null;
                product.PlantDisconts = null;



            }



            #region Catagory
            var categoriesInDb = product.PlantCatagories.Select(bc => bc.CategoryId).ToList();
            var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
            var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

            product.PlantCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

            foreach (var categoryId in categoriesToAdd)
            {
                var productCategory = new PlantCatagory
                {
                    CategoryId = categoryId,
                    Plant = product,
                };

                await _dataContext.PlantCatagories.AddAsync(productCategory);
            }
            #endregion

            #region Tag
            var tagInDb = product.PlantTags.Select(bc => bc.TagId).ToList();
            var tagToRemove = tagInDb.Except(model.TagIds).ToList();
            var tagToAdd = model.TagIds.Except(tagInDb).ToList();

            product.PlantTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


            foreach (var tagId in tagToAdd)
            {
                var productTag = new PlantTag
                {
                    TagId = tagId,
                    Plant = product,
                };

                await _dataContext.PlantTags.AddAsync(productTag);
            }
            #endregion

            #region Brand
            var brandInDb = product.PlantBrands.Select(bc => bc.BrandId).ToList();
            var brandToRemove = brandInDb.Except(model.BrandsIds).ToList();
            var brandToAdd = model.BrandsIds.Except(brandInDb).ToList();

            product.PlantBrands.RemoveAll(bc => brandToRemove.Contains(bc.BrandId));


            foreach (var brandId in brandToAdd)
            {
                var productBrand = new PlantBrand
                {
                    BrandId = brandId,
                    Plant = product,
                };

                await _dataContext.PlantBrands.AddAsync(productBrand);
            }
            #endregion

            #region Discount

            if (model.DiscountIds != null)
            {
                var discountInDb = product.PlantDisconts.Select(bc => bc.DiscontId).ToList();
                var discountToRemove = discountInDb.Except(model.DiscountIds).ToList();
                var discountToAdd = model.DiscountIds.Except(discountInDb).ToList();

                product.PlantDisconts.RemoveAll(bc => discountToRemove.Contains(bc.DiscontId));


                foreach (var discountId in discountToAdd)
                {
                    var productDiscount = new PlantDiscont
                    {
                        DiscontId = discountId,
                        Plant = product,
                    };

                    await _dataContext.PlantDisconts.AddAsync(productDiscount);
                }
            }

            #endregion
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-list");



            IActionResult GetView(UpdateViewModel model)
            {
                model.Categories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();
                model.CategoryIds = product.PlantCatagories.Select(c => c.CategoryId).ToList();


                model.Brands = _dataContext.Brands
                    .Select(b => new BrandListItemViewModel(b.Id, b.Name))
                    .ToList();
                model.BrandsIds = product.PlantBrands.Select(b => b.BrandId).ToList();

                model.Discounts = _dataContext.Disconts
                    .Select(d => new DiscountListViewModel(d.Id, d.Title))
                    .ToList();
                model.DiscountIds = product.PlantDisconts.Select(d => d.Id).ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.TagName))
                 .ToList();

                model.TagIds = product.PlantTags.Select(c => c.TagId).ToList();

                return View(model);
            }


        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-plant-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null)
            {
                return NotFound();
            }

            _dataContext.Plants.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-plant-list");
        }
        #endregion
    }
}
