using GrennyWebApplication.Areas.Client.ViewModels.About;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("about")]
    public class AboutController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("index", Name = "client-about-index")]
        public async Task< IActionResult> IndexAsync()
        {
            var model = new AboutListViewModel
            {
                Abouts = await _dataContext.Abouts.Select(a => new AboutListItemViewModel(
                     a.Context
                   ))
               .ToListAsync(),

                TeamMembers = await _dataContext.TeamMembers.Select(tm => new TeamMemberListItemViewModel(
                    tm.FullName,
                tm.Position,
                    _fileService.GetFileUrl(tm.BgImageNameInFileSystem, UploadDirectory.TeamMember)
                    ))
                 .ToListAsync(),

                FeedBacks = await _dataContext.FeedBacks.Select(fb => new FeedBackListItemViewModel(
                   fb.Id,
                   fb.FullName,
                   fb.Context,
                   fb.Role,
                    _fileService.GetFileUrl(fb.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)
                    ))
                 .ToListAsync(),

            };

            return View(model);
        }
    }
}
