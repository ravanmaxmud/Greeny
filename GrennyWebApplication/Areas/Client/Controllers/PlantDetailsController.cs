using GrennyWebApplication.Areas.Client.ViewModels.Comment;
using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Areas.Client.ViewModels.PlantDetails;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static GrennyWebApplication.Areas.Client.ViewModels.Home.Index.PlantListItemViewModel;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("plantdetails")]
    public class PlantDetailsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public PlantDetailsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("index/{id}", Name = "client-plantdetails-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dbContext.Plants
                .Include(p => p.PlantImages)
                .Include(p => p.PlantDisconts)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            //var catProducts = await _dbContext
            //    .pro.GroupBy(pc => pc.CategoryId).Select(pc => pc.Key).ToListAsync();


            var model = new PlantDetailsViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Content,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                InStock = product.InStock,

             
                Discounts = _dbContext.PlantDisconts.Include(ps => ps.Discont).Where(ps => ps.PlantId == product.Id)
                       .Select(ps => new PlantDetailsViewModel.DiscountList(ps.Discont.Id, ps.Discont.DiscontPers)).ToList(),

                Images = _dbContext.PlantImages.Where(p => p.PlantId == product.Id)
                .Select(p => new PlantDetailsViewModel.ImageViewModeL
                (_fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Plant))).ToList(),


                Products = await _dbContext.PlantCatagories.Include(p => p.Plant).Where(pc => pc.PLantId != product.Id)
                .Select(pc => new PlantListItemViewModel(
                    pc.PLantId,
                    pc.Plant.Title,
                    pc.Plant.Price,
                    pc.Plant.DiscountPrice,
                    pc.Plant.PlantCatagories.Select(pc => new CategoriesList(pc.Category.Title)).ToList(),
                    pc.Plant.PlantImages.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(pc.Plant.PlantImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Plant)
                : String.Empty

               )).ToListAsync(),
                Comment = _dbContext.Comments.Where(p => p.PlantId == product.Id).Select(bc => new CommentViewModel(bc.Id, bc.Name, bc.Email, bc.Context, bc.CreatedAt)).ToList(),

            };

            return View(model);
        }

        [HttpPost("comment/{plantId}", Name = "client-plantdetails-comment")]
        public async Task<IActionResult> IndexAsync(int plantId, [FromForm] PlantDetailsViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var product = await _dbContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
            if (product == null)
            {
                return NotFound();
            }
            var model = new Comment
            {
                PlantId = plantId,
                Name = commentViewModel.Name,
                Email = commentViewModel.Email,
                Context = commentViewModel.Context,



            };

            await _dbContext.Comments.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-plantdetails-index", new { id = plantId });
        }
    }
}
