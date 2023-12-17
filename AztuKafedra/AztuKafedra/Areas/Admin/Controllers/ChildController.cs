using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IActionResult> Update(int? id)
        {
            ChildCategory child = await _context.ChildCategory.FindAsync(id);
            List<ParentCategory> Parent = _context.Parentcategory.Include(m => m.BigParentsCategory).ToList();
            List<SelectListItem> Select = Parent.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} - {m.BigParentsCategory.Name}"
            }).ToList();

            ViewBag.Childs = Select;
            ChildUpdateVM updateVM = new ChildUpdateVM()
            {
                Id = child.Id,
                Name = child.Name,
                Title=child.Title,
                Description = child.Description,
                ParentCategoryId=child.ParentCategoryId,
            };

            return View("Update", updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ChildUpdateVM updateVM)
        {
            List<ParentCategory> Parent = _context.Parentcategory.Include(m => m.BigParentsCategory).ToList();
            List<SelectListItem> Select = Parent.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} - {m.BigParentsCategory.Name}"
            }).ToList();

            ViewBag.Childs = Select;
            if (!updateVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", $"{updateVM.Photo.FileName} Şekil Tipinde Olmalıdır");
                ViewBag.Childs = await _context.ChildCategory
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
                    .ToListAsync();

                return View(updateVM);
            }

            if (!updateVM.Photo.CheckFileSize(1800))
            {
                ModelState.AddModelError("Photo", $"{updateVM.Photo.FileName} - 200kb'dan Fazla Olamaz");
                ViewBag.Childs = await _context.ChildCategory
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
                    .ToListAsync();

                return View(updateVM);
            }

            ChildCategory child = await _context.ChildCategory.FindAsync(updateVM.Id);

            if (child == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "image", "ChildImage");
            string oldFilePath = Path.Combine(rootPath, child.ImagePath);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            string root = Path.Combine(_env.WebRootPath, "image", "ChildImage");
            string fileName = await updateVM.Photo.SaveAsync(root);

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                await updateVM.Photo.CopyToAsync(fileStream);
            }
            child.Name = updateVM.Name;
            child.ImagePath = fileName;
            child.Title = updateVM.Title;
            child.Description = updateVM.Description;
            child.ParentCategoryId=updateVM.ParentCategoryId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            ChildCategory child = await _context.ChildCategory.FindAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "image", "ChildImage");
            string oldFilePath = Path.Combine(rootPath, child.ImagePath);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            if (child is null) return NotFound();
            _context.ChildCategory.Remove(child);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
