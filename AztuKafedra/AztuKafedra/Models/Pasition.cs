using AztuKafedra.Models.BaseId;

namespace AztuKafedra.Models
{
    public class Pasition:Base
    {
        public string Name { get; set; }

        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
       
    }
}
