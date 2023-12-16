using AztuKafedra.Models;

namespace AztuKafedra.ViewModel
{
    public class ParentVM
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public int BigParentsCategoryId { get; set; }
    }
}
