using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public NewsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.News.Include(d=>d.ChildCategory).ToList());
        }
        public IActionResult Create()
        {
            List<ChildCategory> child = _context.ChildCategory.ToList();
            List<SelectListItem> childiTEMS = child.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.NEWS = childiTEMS;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewVM newVM)
        {

            if (!newVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{newVM.Photo.FileName} Sekil Tipinde olmalidir ");
                return View();
            }
            if (!newVM.Photo.CheckFileSize(1500))
            {
                ModelState.AddModelError("Photo", $"{newVM.Photo.FileName} - 200kb dan Cox Olmaz");
                return View();
            }

            string root = Path.Combine(_env.WebRootPath, "image", "News");
            string fileName = await newVM.Photo.SaveAsync(root);

            News New = new News { Title = newVM.Title, Description= newVM.Description,ImagePath= fileName, ViewCount=newVM.ViewCount, DateTime = DateTime.Now, ChildCategoryId=newVM.ChildCategoryId};
            _context.News.Add(New);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
