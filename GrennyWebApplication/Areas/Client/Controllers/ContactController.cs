using BackEndFinalProject.Database.Models;
using GrennyWebApplication.Areas.Client.ViewModels.Contact;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ContactController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "client-contact-list")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new ContactViewModel
            {
                Cities = await _dataContext.Cities.Select(ci => new CityViewModel(
                    ci.Name,
                    ci.Address,
                    _fileService.GetFileUrl(ci.BgImageNameInFileSystem, UploadDirectory.City)
                    ))
                 .ToListAsync()
            };
            return View(model);
        }
        [HttpPost("list", Name = "client-contact-list")]
        public async Task<IActionResult> IndexAsync([FromForm] ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = new Contact
            {
                FirstName = contactViewModel.FirstName,
                Subject = contactViewModel.Subject,
                Email = contactViewModel.Email,
                Message = contactViewModel.Message,


            };

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }

    }
}
