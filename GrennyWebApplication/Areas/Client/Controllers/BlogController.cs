using GrennyWebApplication.Areas.Client.ViewModels.Home;
using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;
using GrennyWebApplication.Contracts.File;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public BlogController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("index", Name = "client-blog-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Blogs = await _dbContext.Blogs.Include(b => b.BlogTags).Select(b => new BlogListItemViewModel(b.Id, b.Title, b.Description,
                      b.BlogFile!.Take(1)!.FirstOrDefault() != null
                            ? _fileService.GetFileUrl(b.BlogFile!.Take(1)!.FirstOrDefault()!.FileNameInFileSystem!, UploadDirectory.Blog)
                             : string.Empty, b.CreatedAt)).ToListAsync(),
                BlogCategories = await _dbContext.BlogCategories.Select(b=> new BlogCategoryViewModel(b.Id,b.Title,
                b.BlogCatagories.Where(p=> p.BlogCategoryId == b.Id).Count())).ToListAsync(),

                BlogTags = await _dbContext.BlogTags.Select(b=> new BlogTagViewModel(b.Id,b.TagName)).ToListAsync()
            };
            return View(model);
        }
    }
}
