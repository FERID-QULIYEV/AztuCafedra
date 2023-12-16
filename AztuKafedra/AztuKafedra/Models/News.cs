using AztuKafedra.Models.BaseId;

namespace AztuKafedra.Models
{
    public class News : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }
        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
    }
}
