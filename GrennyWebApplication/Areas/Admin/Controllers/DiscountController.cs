﻿using GrennyWebApplication.Database.Models;
using GrennyWebApplication.Database;
using GrennyWebApplication.Areas.Admin.ViewModels.Discount;
using GrennyWebApplication.Database;
using GrennyWebApplication.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/discount")]
    public class DiscountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(DataContext dataContext, ILogger<DiscountController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-discount-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Disconts
                .Select(c => new ListItemViewModel(c.Id, c.Title, c.DiscontPers, c.DiscountTime, c.CreatedAt, c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-discount-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-discount-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var discount = new Discont
            {
                Title = model.Title,
                DiscontPers = model.DiscontPers,
                DiscountTime = model.DiscountTime,
            };
            await _dataContext.Disconts.AddAsync(discount);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-discount-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-discount-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var discount = await _dataContext.Disconts.FirstOrDefaultAsync(n => n.Id == id);


            if (discount is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Title = discount.Title,
                DiscontPers = discount.DiscontPers,
                DiscountTime = discount.DiscountTime,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-discount-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var discount = await _dataContext.Disconts.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (discount is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.Disconts.Any(n => n.Id == model.Id)) return View(model);

            discount.Title = model.Title;
            discount.DiscontPers = model.DiscontPers;
            discount.DiscountTime = model.DiscountTime;
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-discount-list");

        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-discount-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var discount = await _dataContext.Disconts.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (discount is null) return NotFound();
            _dataContext.Disconts.Remove(discount);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-discount-list");

        }
        #endregion
    }
}