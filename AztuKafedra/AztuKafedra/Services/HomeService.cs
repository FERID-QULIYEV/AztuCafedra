using AztuKafedra.DAL;
using AztuKafedra.ViewModel;
using Microsoft.EntityFrameworkCore;
using AztuKafedra.ViewModel;
namespace AztuKafedra.Services
{
    

    public class HomeService
    {
       private readonly AppDbContext _db;

        public HomeService(AppDbContext db)
        {
            _db = db;
        }
        public HomeVM GetHomeData()
        {
            var bigParentsCategories = _db.BigParentsCategory
                .Include(bp => bp.ParentCategories)
                    .ThenInclude(pc => pc.ChildCategories)
                    .ThenInclude(cc => cc.Users) // Users özelliğini yükle
                .ToList();

            var homeVm = new HomeVM
            {
                News=_db.News.ToList(),
                Sliders = _db.Sliders.ToList(),
                BigParentsCategories = bigParentsCategories,
                ChildCategories = _db.ChildCategory.Include(d => d.ParentCategory).Include(cc => cc.Users).Include(nw=>nw.News).ToList(),
                ParentCategories = _db.Parentcategory.ToList(),
            };
            
            return homeVm;
        }

        public HomeVM GetHomeDataForChildCategory(int childCategoryId)
        {
            var bigParentsCategories = _db.BigParentsCategory
                .Include(bp => bp.ParentCategories)
                    .ThenInclude(pc => pc.ChildCategories)
                .ToList();

            var homeVm = new HomeVM
            {
                BigParentsCategories = bigParentsCategories,
                ChildCategories = _db.ChildCategory
                    .Include(d => d.ParentCategory)
                    .Include(c => c.Users) // Users özelliğini yükle
                    .ToList(),
                ParentCategories = _db.Parentcategory.ToList(),
                // Seçilen ChildCategory'ye özgü verileri ekleyin
                SelectedChildCategory = _db.ChildCategory
                    .Include(c => c.Users) // Users özelliğini yükle
                    .FirstOrDefault(c => c.Id == childCategoryId)
            };

            return homeVm;
        }


    }
}
