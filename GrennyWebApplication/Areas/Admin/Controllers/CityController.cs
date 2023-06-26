using GrennyWebApplication.Areas.Admin.ViewModels.City;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/city")]
    public class CityController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public CityController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("list", Name = "admin-city-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Cities
                .Select(u => new ListViewModel(
                  u.Id, u.Name, u.Address, u.CreatedAt, u.UpdatedAt, _fileService.GetFileUrl(u.BgImageNameInFileSystem, UploadDirectory.City)))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-city-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }



        [HttpPost("add", Name = "admin-city-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.City);

            await AddTeamMember(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-city-list");


            async Task AddTeamMember(string imageName, string imageNameInSystem)
            {
                var city = new City
                {
                    Name = model.Name,
                    Address = model.Address,
                    BgImageName = imageName,
                    BgImageNameInFileSystem = imageNameInSystem,
                };

                await _dataContext.Cities.AddAsync(city);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-city-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync(b => b.Id == id);
            if (city is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Address = city.Address,
                ImageUrl = _fileService.GetFileUrl(city.BgImageNameInFileSystem, UploadDirectory.City)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-city-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (city is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(city.BgImageNameInFileSystem, UploadDirectory.City);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.City);
                await UpdateTeamMemberAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateTeamMemberAsync(city.BgImageName, city.BgImageNameInFileSystem);
            }


            return RedirectToRoute("admin-city-list");


            async Task UpdateTeamMemberAsync(string imageName, string imageNameInFileSystem)
            {
                city.Name = model.Name;
                city.Address = model.Address;
                city.BgImageName = imageName;
                city.BgImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion


    }
}
