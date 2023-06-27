using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new IndexViewModel
            {

                Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderLIstItemViewModel(
                    b.Title,
                    b.OfferContext,
                    b.Content,
                    b.ButtonName,
                    _fileService.GetFileUrl(b.BgImageNameInFileSystem, UploadDirectory.Slider),
                    b.BtnRedirectUrl,
                    b.Order
                    ))
                     .ToListAsync(),

                Categories = await _dbContext.Categories
                     .Select(c => new CategoryViewModel(c.Id, c.Title,
                     c.Catagories.Select(s => new SubCategoryViewModel(s.Id, s.Title)).ToList())).ToListAsync(),

                FeedBacks = await _dbContext.FeedBacks.Select(f => new FeedBackListItemViewModel(
                    f.Id,
                    f.FullName,
                    f.Context,
                    f.Role,
                   _fileService.GetFileUrl(f.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)
                    ))
                      .ToListAsync(),

                GlobalOffers = await _dbContext.Disconts.Take(1).Select(go => new GlobalOfferViewModel(
                       go.Title,
                       go.DiscountTime)).ToListAsync(),

                Plants = await _dbContext.Plants.Include(p => p.PlantImages)
                .Select(p => new PlantViewModel(p.Id, p.Title, p.Price, p.DiscountPrice, p.Content,
                p.PlantImages.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.PlantImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Plant) : String.Empty
                )).ToListAsync(),


                Blogs = await _dbContext.Blogs.Include(b => b.BlogTags).Select(b => new BlogListItemViewModel(b.Id, b.Title, b.Description,
                b.BlogFile!.Take(1)!.FirstOrDefault() != null
                      ? _fileService.GetFileUrl(b.BlogFile!.Take(1)!.FirstOrDefault()!.FileNameInFileSystem!, UploadDirectory.Blog)
                       : string.Empty,b.CreatedAt)).ToListAsync(),
            };
            return View(model);
        }

        //[HttpGet("modal/{id}", Name = "plant-modal")]
        //public async Task<ActionResult> ModalAsync(int id)
        //{
        //    var plant = await _dbContext.Plants.Include(p => p.PlantImages)
        //        .Include(p => p.PlantTags)
        //        .Include(p => p.PlantCatagories).FirstOrDefaultAsync(p => p.Id == id);


        //    if (plant is null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ModalViewModel(plant.Id, plant.Title, plant.Content, plant.Price,
        //        plant.PlantImages!.Take(1).FirstOrDefault() != null
        //        ? _fileService.GetFileUrl(plant.PlantImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Plant)
        //        : String.Empty,
        //        _dbContext.PlantColors.Include(pc => pc.Color).Where(pc => pc.PlantId == plant.Id)
        //        .Select(pc => new ModalViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToList(),
        //        _dbContext.PlantSizes.Include(ps => ps.Size).Where(ps => ps.PlantId == plant.Id)
        //        .Select(ps => new ModalViewModel.SizeViewModeL(ps.Size.Name, ps.Size.Id)).ToList()
        //        );

        //    return PartialView("~/Areas/Client/Views/Shared/Partials/_ModelPartial.cshtml", model);
        //}

        //[HttpGet("indexsearch", Name = "client-homesearch-index")]
        //public async Task<IActionResult> Search(string searchBy, string search)
        //{

        //    return RedirectToAction("Index", "ShopPage", new { searchBy = searchBy, search = search });

        //}
        [HttpGet("indexsearch", Name = "client-homesearch-index")]
        public async Task<IActionResult> Search(string searchBy, string search, int? categoryId = null, int? tagId = null)
        {

            return RedirectToRoute("client-shop-index", new { searchBy = searchBy, search = search, categoryId = categoryId, tagId = tagId });

        }

    }

}

