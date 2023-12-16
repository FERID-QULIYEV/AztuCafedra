using AztuKafedra.Models.BaseId;

namespace AztuKafedra.Models
{
    public class BigParentsCategory:Base
    {
        public string Name { get; set; }
        public List<ParentCategory>? ParentCategories { get; set; }

    }
}
