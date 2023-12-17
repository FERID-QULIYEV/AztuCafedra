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
            ParentCategory parent = new ParentCategory{ Name=parentVM.Name, DateTime=DateTime.Now, BigParentsCategoryId=parentVM.BigParentsCategoryId};
            _context.Parentcategory.Add(parent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            List<BigParentsCategory> BigParents = _context.BigParentsCategory.ToList();
            List<SelectListItem> BigparentiTEMs = BigParents.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Parent = BigparentiTEMs;
            if (id == null || id == 0) return BadRequest();
            ParentCategory parent = _context.Parentcategory.Find(id);
            if (parent is null) return NotFound();
            return View(parent);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, ParentCategory Parent)
        {
            if (id == null || id == 0 || id != Parent.Id || Parent is null) return BadRequest();
            ParentCategory exist = _context.Parentcategory.Find(Parent.Id);
            exist.Name = Parent.Name;
            exist.BigParentsCategoryId = Parent.BigParentsCategoryId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            ParentCategory parent= _context.Parentcategory.Find(id);
            if (parent is null) return NotFound();
            //if (parent.ChildCategories is null)
            //{
            //    _context.Parentcategory.Remove(parent);
            //}

            _context.Parentcategory.Remove(parent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
