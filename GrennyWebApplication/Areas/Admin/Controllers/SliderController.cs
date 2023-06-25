﻿using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using GrennyWebApplication.Areas.Admin.ViewModels.Slider;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GrennyWebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    [Authorize(Roles = "admin")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }



        #region List
        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders
                .Select(u => new ListSliderViewModel(
                  u.Id, u.Title, u.OfferContext, u.Content, u.ButtonName, u.BtnRedirectUrl, u.Order, u.CreatedAt, u.UpdatedAt,_fileService.GetFileUrl(u.BgImageNameInFileSystem,UploadDirectory.Slider)))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync(AddSliderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Slider);

            await AddSlider(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-slider-list");


            async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var slider = new Slider
                {
                    Title = model.Title,
                    OfferContext = model.OfferContext,
                    Content = model.Content,
                    ButtonName = model.ButtonName,
                    BtnRedirectUrl = model.ButtonRedirectUrl,
                    Order = model.Order,
                    BgImageName = imageName,
                    BgImageNameInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.Sliders.AddAsync(slider);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }

            var model = new AddSliderViewModel
            {
                Id = slider.Id,
                Title = slider.Title,
                OfferContext = slider.OfferContext,
                Content = slider.Content,
                ButtonName = slider.ButtonName,
                ButtonRedirectUrl = slider.BtnRedirectUrl,
                Order = slider.Order,
                ImageUrl = _fileService.GetFileUrl(slider.BgImageNameInFileSystem, UploadDirectory.Slider)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(AddSliderViewModel model)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(slider.BgImageNameInFileSystem, UploadDirectory.Slider);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);
                await UpdateSliderAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateSliderAsync(slider.BgImageName, slider.BgImageNameInFileSystem);
            }


            return RedirectToRoute("admin-slider-list");


            async Task UpdateSliderAsync(string imageName, string imageNameInFileSystem)
            {
                slider.Title = model.Title;
                slider.OfferContext = model.OfferContext;
                slider.Content = model.Content;
                slider.ButtonName = model.ButtonName;
                slider.BtnRedirectUrl = model.ButtonRedirectUrl;
                slider.Order = model.Order;
                slider.BgImageName = imageName;
                slider.BgImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(slider.BgImageNameInFileSystem, UploadDirectory.Slider);

            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        } 
        #endregion
    }
}
