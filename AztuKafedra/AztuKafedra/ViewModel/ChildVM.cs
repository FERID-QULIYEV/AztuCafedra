using AztuKafedra.Models;
using AztuKafedra.Models.BaseId;

namespace AztuKafedra.ViewModel
{
    public class ChildVM:Base
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
