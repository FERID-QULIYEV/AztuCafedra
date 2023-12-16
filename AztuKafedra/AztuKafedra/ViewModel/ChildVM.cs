using AztuKafedra.Models;

namespace AztuKafedra.ViewModel
{
    public class ChildVM
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }
        //public int BigParentsCategoryId { get; set; }
    }
}
