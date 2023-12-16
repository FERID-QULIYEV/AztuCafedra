using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace AztuKafedra.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ChildController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public ChildController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var child = _context.ChildCategory.Include(d=>d.ParentCategory).Include(d => d.ParentCategory.BigParentsCategory).ToList();
            return View(child);
        }
        public async Task<IActionResult> Create()
        {
            List<ParentCategory> Parent = _context.Parentcategory.Include(m => m.BigParentsCategory).ToList();
            List<SelectListItem> Select = Parent.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} - {m.BigParentsCategory.Name}"
            }).ToList();

            ViewBag.Childs = Select;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChildVM childVM)
        {
            if (!childVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{childVM.Photo.FileName} Sekil Tipinde olmalidir ");
                return View();
            }
            if (!childVM.Photo.CheckFileSize(1500))
            {
                ModelState.AddModelError("Photo", $"{childVM.Photo.FileName} - 200kb dan Cox Olmaz");
                return View();
            }

            string root = Path.Combine(_env.WebRootPath, "image", "ChildImage");
            string fileName = await childVM.Photo.SaveAsync(root);

            ChildCategory child = new ChildCategory { Name = childVM.Name, DateTime = DateTime.Now , Description=childVM.Description, ImagePath= fileName, Title =childVM.Title, ParentCategoryId=childVM.ParentCategoryId};
            _context.ChildCategory.Add(child);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            ChildCategory child= _context.ChildCategory.Find(id);
            if (child is null) return NotFound();
            _context.ChildCategory.Remove(child);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
