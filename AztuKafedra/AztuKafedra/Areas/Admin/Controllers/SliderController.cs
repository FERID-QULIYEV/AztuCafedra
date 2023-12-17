using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SliderVM sliderVM)
        {
            if (!sliderVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{sliderVM.Photo.FileName} Sekil Tipinde olmalidir ");
                return View();
            }
            if (!sliderVM.Photo.CheckFileSize(1500))
            {
                ModelState.AddModelError("Photo", $"{sliderVM.Photo.FileName} - 200kb dan Cox Olmaz");
                return View();
            }

            string root = Path.Combine(_env.WebRootPath, "image", "ImageSlider");
            string fileName = await sliderVM.Photo.SaveAsync(root);

            Slider slider = new Slider { ImagePath=fileName, DateTime = DateTime.Now};
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update()
        {
            SliderUpdateVM updateVM = new SliderUpdateVM()
            {
            };

            return View("Update", updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SliderUpdateVM updateVM)
        {
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

            Slider slider = await _context.Sliders.FindAsync(updateVM.Id);

            if (slider == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "image", "ImageSlider");
            string oldFilePath = Path.Combine(rootPath, slider.ImagePath);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            string root = Path.Combine(_env.WebRootPath, "image", "ImageSlider");
            string fileName = await updateVM.Photo.SaveAsync(root);

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                await updateVM.Photo.CopyToAsync(fileStream);
            }
            slider.ImagePath = fileName;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider= await _context.Sliders.FindAsync(id);

            if (slider == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "image", "ImageSlider");
            string oldFilePath = Path.Combine(rootPath, slider.ImagePath);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            if (slider is null) return NotFound();
            _context.Sliders.Remove(slider);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
