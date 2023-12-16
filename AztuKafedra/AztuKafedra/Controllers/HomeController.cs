using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AztuKafedra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly HomeService _homeService;


        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, HomeService homeService)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _homeService = homeService;
        }

        public IActionResult Index()
        {

            var homeVm = _homeService.GetHomeData();
            ViewBag.dbServiceCount = _appDbContext.News.Count();
            return View(homeVm);


        }
        public IActionResult Fakulteler(int id)
        {
            // İlgili ChildCategory'yi bulun
            var childCategory = _appDbContext.ChildCategory.Include(c => c.ParentCategory).Include(d=>d.Users).FirstOrDefault(c => c.Id == id);

            if (childCategory == null)
            {
                return NotFound();
            }

            var homeVm = _homeService.GetHomeDataForChildCategory(id);
            return View(homeVm);
        }
        public IActionResult Kafedra(int id)
        {
            // İlgili ChildCategory'yi bulun
            var childCategory = _appDbContext.ChildCategory.Include(c => c.ParentCategory).Include(d => d.Users).FirstOrDefault(c => c.Id == id);

            if (childCategory == null)
            {
                return NotFound();
            }

            var homeVm = _homeService.GetHomeDataForChildCategory(id);
            return View(homeVm);
        }

        public IActionResult News()
        {
            var homeVm = _homeService.GetHomeData();
            ViewBag.dbServiceCount = _appDbContext.News.Count();
            return View(homeVm);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
