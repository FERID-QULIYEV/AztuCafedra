using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using System;
using AztuKafedra.DAL;
using AztuKafedra.ViewModel;
using AztuKafedra.Models;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BigParentController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public BigParentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.BigParentsCategory.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BigParentVM BigParentVM)
        {
            BigParentsCategory bigParent = new BigParentsCategory { Name= BigParentVM.Name ,DateTime=DateTime.Now };
            _context.BigParentsCategory.Add(bigParent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            BigParentsCategory bigparent= _context.BigParentsCategory.Find(id);
            if (bigparent is null) return NotFound();
            return View(bigparent);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, BigParentsCategory bigParent)
        {
            if (id == null || id == 0 || id != bigParent.Id || bigParent is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            BigParentsCategory exist = _context.BigParentsCategory.Find(bigParent.Id);
            exist.Name = bigParent.Name;
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
