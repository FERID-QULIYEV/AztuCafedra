using AztuKafedra.DAL;
using AztuKafedra.Models;
using AztuKafedra.Utilities;
using AztuKafedra.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AztuKafedra.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public UserController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var user = _context.User.Include(u=>u.ChildCategory).ToList();
            return View(user);
        }
        public async Task<IActionResult> Create()
        {
            List<ChildCategory> child = _context.ChildCategory.ToList();
            List<SelectListItem> useriTEMS = child.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Users = useriTEMS;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {

            if (!userVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{userVM.Photo.FileName} Sekil Tipinde olmalidir ");
                return View();
            }
            if (!userVM.Photo.CheckFileSize(1500))
            {
                ModelState.AddModelError("Photo", $"{userVM.Photo.FileName} - 200kb dan Cox Olmaz");
                return View();
            }

            string root = Path.Combine(_env.WebRootPath, "image", "UserImage");
            string fileName = await userVM.Photo.SaveAsync(root);

            User user = new User { Name = userVM.Name, DateTime = DateTime.Now, Description = userVM.Description, ImagePath = fileName, Email= userVM.Email, Phonenumber= userVM.Phonenumber , ChildCategoryId= userVM.ChildCategoryId};
            _context.User.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Update(int? id)
        //{
        //    User user = await _context.User.FindAsync(id);
        //    List<ChildCategory> child = _context.ChildCategory.ToList();
        //    List<SelectListItem> useriTEMS = child.Select(p => new SelectListItem
        //    {
        //        Value = p.Id.ToString(),
        //        Text = p.Name
        //    }).ToList();

        //    ViewBag.Users = useriTEMS;
        //    UserUpdateVM updateVM = new UserUpdateVM()
        //    {
        //        Name=user.Name,
        //        Phonenumber=user.Phonenumber,
        //        ChildCategoryId=user.ChildCategoryId,
        //        Email=user.Email,
        //        Description=user.Description,
        //    };

        //    return View("Update", updateVM);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Update(UserUpdateVM updateVM)
        //{
        //    List<ChildCategory> child= _context.ChildCategory.ToList();
        //    List<SelectListItem> Select = child.Select(m => new SelectListItem
        //    {
        //        Value = m.Id.ToString(),
        //        Text = m.Name
        //    }).ToList();

        //    ViewBag.Users = Select;
        //    if (!updateVM.Photo.ContentType.Contains("image/"))
        //    {
        //        ModelState.AddModelError("Photo", $"{updateVM.Photo.FileName} Şekil Tipinde Olmalıdır");
        //        ViewBag.Childs = await _context.ChildCategory
        //            .Select(p => new SelectListItem
        //            {
        //                Value = p.Id.ToString(),
        //                Text = p.Name
        //            })
        //            .ToListAsync();

        //        return View(updateVM);
        //    }

        //    if (!updateVM.Photo.CheckFileSize(1800))
        //    {
        //        ModelState.AddModelError("Photo", $"{updateVM.Photo.FileName} - 200kb'dan Fazla Olamaz");
        //        ViewBag.Childs = await _context.ChildCategory
        //            .Select(p => new SelectListItem
        //            {
        //                Value = p.Id.ToString(),
        //                Text = p.Name
        //            })
        //            .ToListAsync();

        //        return View(updateVM);
        //    }

        //    ChildCategory child = await _context.ChildCategory.FindAsync(updateVM.Id);

        //    if (child == null)
        //    {
        //        return NotFound();
        //    }

        //    string rootPath = Path.Combine(_env.WebRootPath, "image", "ChildImage");
        //    string oldFilePath = Path.Combine(rootPath, child.ImagePath);

        //    if (System.IO.File.Exists(oldFilePath))
        //    {
        //        System.IO.File.Delete(oldFilePath);
        //    }

        //    string root = Path.Combine(_env.WebRootPath, "image", "ChildImage");
        //    string fileName = await updateVM.Photo.SaveAsync(root);

        //    using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        //    {
        //        await updateVM.Photo.CopyToAsync(fileStream);
        //    }
        //    child.Name = updateVM.Name;
        //    child.ImagePath = fileName;
        //    child.Title = updateVM.Title;
        //    child.Description = updateVM.Description;
        //    child.ParentCategoryId = updateVM.ParentCategoryId;
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            ChildCategory child = await _context.ChildCategory.FindAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "image", "UserImage");
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
