using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PasitionController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public PasitionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Pasitions.Include(p=>p.ChildCategory).ToList());
        }
        public IActionResult Create()
        {
            List<ChildCategory> child = _context.ChildCategory.ToList();
            List<SelectListItem> childtiTEMS = child.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Pasitions = childtiTEMS;
            return View();
        }
        [HttpPost]
        public IActionResult Create(PasitionVM pasitionVM)
        {

            Pasition pasition = new Pasition { Name = pasitionVM.Name, DateTime = DateTime.Now, ChildCategoryId = pasitionVM.ChildCategoryId };
            _context.Pasitions.Add(pasition);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            List<ChildCategory> child = _context.ChildCategory.ToList();
            List<SelectListItem> childtiTEMS = child.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Pasitions = childtiTEMS;
            if (id == null || id == 0) return BadRequest();
            Pasition pasition = _context.Pasitions.Find(id);
            if (pasition is null) return NotFound();
            return View(pasition);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Pasition pasition)
        {
            if (id == null || id == 0 || id != pasition.Id || pasition is null) return BadRequest();
            Pasition exist = _context.Pasitions.Find(id);
            exist.Name = pasition.Name;
            exist.ChildCategoryId= pasition.ChildCategoryId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            BigParentsCategory bigparent = _context.BigParentsCategory.Find(id);
            if (bigparent is null) return NotFound();
            //if (bigparent.ParentCategories is null)
            //{
            //    _context.BigParentsCategory.Remove(bigparent);
            //}
            _context.BigParentsCategory.Remove(bigparent);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
