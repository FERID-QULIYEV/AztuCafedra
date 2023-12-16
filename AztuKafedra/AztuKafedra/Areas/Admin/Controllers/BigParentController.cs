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

    }
}
