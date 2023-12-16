using AztuKafedra.Models;

namespace AztuKafedra.ViewModel
{
    public class HomeVM
    {
        public List<ParentCategory>? ParentCategories { get; set; }
        public List<ChildCategory>? ChildCategories { get;  set; }
        public List<BigParentsCategory>? BigParentsCategories { get; set; }
        public ChildCategory? SelectedChildCategory { get; set; }
        public List<User>? Users { get; set; }   
        public List<Pasition>? Pasitions { get; set; }
        public List<Slider>? Sliders { get; set; }
        public List<News> News { get; set; }
    }
}
