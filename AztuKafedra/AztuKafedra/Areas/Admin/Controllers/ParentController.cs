using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Drawing2D;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParentController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public ParentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Parentcategory.ToList());
        }
        public IActionResult Create()
        {
            List<BigParentsCategory> bigParents = _context.BigParentsCategory.ToList();
            List<SelectListItem> BigparentiTEMS = bigParents.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Parents = BigparentiTEMS;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ParentVM parentVM)
        {
            if (!parentVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{parentVM.Photo.FileName} Sekil Tipinde olmalidir ");
                return View();
            }
            if (!parentVM.Photo.CheckFileSize(1500))
            {
                ModelState.AddModelError("Photo", $"{parentVM.Photo.FileName} - 200kb dan Cox Olmaz");
                return View();
            }

            string root = Path.Combine(_env.WebRootPath, "RootAllPictures", "img");
            string fileName = await parentVM.Photo.SaveAsync(root);

            ParentCategory parent = new ParentCategory{ Name=parentVM.Name, DateTime=DateTime.Now, BigParentsCategoryId=parentVM.BigParentsCategoryId};
            _context.Parentcategory.Add(parent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
