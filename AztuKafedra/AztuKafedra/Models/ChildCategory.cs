using AztuKafedra.Models.BaseId;
using System.Data;

namespace AztuKafedra.Models
{
    public class ChildCategory:Base
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int BigParentsCategoryId { get; set; }
        //public BigParentsCategory BigParentsCategory { get; set; }
        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }
        public List<News> News { get; set; }
        public List<Pasition> Pasitions { get; set; }
        public List<User> Users { get; set; }

    }
}
