using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async  Task<IActionResult> IndexAsync()
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
            };
            return View(model);


            //var model = new IndexViewModel
            //{

            //    Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderLIstItemViewModel(
            //        b.Title,
            //        b.OfferContext,
            //        b.Content,
            //        b.ButtonName,
            //        _fileService.GetFileUrl(b.BgImageNameInFileSystem, UploadDirectory.Slider),
            //        b.BtnRedirectUrl,
            //        b.Order

            //        ))
            //    .ToListAsync(),

            //Payments=await _dbContext.Payments.Select(p=>new PaymmentLIstItemViewModel(
            //    p.Id,
            //    p.Title,
            //    p.Context,
            //    _fileService.GetFileUrl(p.ImageNameInFileSystem,UploadDirectory.Payment)
            //        ))
            //   .ToListAsync(),

            //FeedBacks=await _dbContext.FeedBacks.Select(f=>new FeedBackListItemViewModel(
            //    f.Id,
            //    f.FullName,
            //    f.Context,
            //    f.Role,
            //   _fileService.GetFileUrl(f.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)

            //    ))
            //.ToListAsync(),

            //Rewards=await _dbContext.Rewards.Select(r=>new RewardLIstItemViewModel(
            //    r.Id,
            //    _fileService.GetFileUrl(r.BgImageNameInFileSystem,UploadDirectory.Reward)
            //    ))
            //.ToListAsync(),

            //Blogs=await _dbContext.Blogs.Include(b=>b.BlogFile).Select(b=>new BlogListItemViewModel(
            //b.Id,
            //b.Title,
            //b.Description,

            //b.BlogFile!.Take(1).FirstOrDefault() != null
            //? _fileService.GetFileUrl(b.BlogFile.Take(1).FirstOrDefault()!.FileNameInFileSystem, UploadDirectory.Blog)
            //: String.Empty,
            //b.BlogFile.FirstOrDefault().IsShowImage,
            //b.BlogFile.FirstOrDefault().IsShowVideo,
            //b.CreatedAt

            //    ))
            //.ToListAsync(),


            //};
            return View();
        }

        //[HttpGet("modal/{id}", Name = "plant-modal")]
        //public async Task<ActionResult> ModalAsync(int id)
        //{
        //    var plant = await _dbContext.Plants.Include(p => p.PlantImages)
        //        .Include(p => p.PlantColors)
        //        .Include(p => p.PlantSizes).FirstOrDefaultAsync(p => p.Id == id);


        //    if (plant is null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ModalViewModel(plant.Id ,plant.Title, plant.Content, plant.Price,
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
    }

}

