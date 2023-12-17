using AztuKafedra.Models;
using AztuKafedra.Models.BaseId;
using System.ComponentModel.DataAnnotations;

namespace AztuKafedra.ViewModel
{
    public class ChildUpdateVM:Base
    {

        [Required, MaxLength(30)]
        public string? Name { get; set; }
        public IFormFile Photo { get; set; }
        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string Description { get; set; }
        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }
    }
}
