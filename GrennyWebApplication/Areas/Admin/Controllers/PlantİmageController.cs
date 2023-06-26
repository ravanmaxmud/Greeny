

using GrennyWebApplication.Areas.Admin.ViewModels.Plantİmage;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GrennyWebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/plantimg")]
    [Authorize(Roles = "admin")]
    public class PlantİmageController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public PlantİmageController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("{pLantId}/image/list", Name = "admin-plantimg-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int plantId)
        {
            var plant = await _dataContext.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == plantId);
            if (plant == null) return NotFound();

            var model = new PlantİmageViewModel { PlantId = plant.Id };

            model.Images = plant.PlantImages.Select(p => new PlantİmageViewModel.ListItem
            {
                Id = p.Id,
                ImageUrl = _fileService.GetFileUrl(p.ImageNameInFileSystem, Contracts.File.UploadDirectory.Plant),
                CreatedAt = p.CreatedAt
            }).ToList();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("{plantId}/image/add", Name = "admin-plantimg-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("{plantId}/image/add", Name = "admin-plantimg-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int plantId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var plant = await _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);

            if (plant is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Plant);

            var plantImage = new PlantImage
            {
                Plant = plant,
                ImageName = model.Image.FileName,
                ImageNameInFileSystem = imageNameInSystem,
            };

            await _dataContext.PlantImages.AddAsync(plantImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plantimg-list", new { plantId = plantId });

        } 
        #endregion

        #region Delete

        [HttpPost("{plantId}/image/{plantImageId}/delete", Name = "admin-plantimg-delete")]
        public async Task<IActionResult> Delete([FromRoute] int plantId, [FromRoute] int plantImageId)
        {

            var plantImage = await _dataContext.PlantImages.FirstOrDefaultAsync(p => p.PlantId == plantId && p.Id == plantImageId);

            if (plantImage is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(plantImage.ImageNameInFileSystem, UploadDirectory.Plant);

            _dataContext.PlantImages.Remove(plantImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-productimg-list", new { plantId = plantId });

        }
    } 
    #endregion
}
