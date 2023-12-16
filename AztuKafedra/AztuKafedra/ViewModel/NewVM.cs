using AztuKafedra.Models;

namespace AztuKafedra.ViewModel
{
    public class NewVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public int ViewCount { get; set; }
        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
    }
}
