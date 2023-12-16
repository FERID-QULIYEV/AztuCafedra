using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
    }
}
