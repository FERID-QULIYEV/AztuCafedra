using AztuKafedra.Models;
using System.ComponentModel.DataAnnotations;

namespace AztuKafedra.ViewModel
{
    public class UserUpdateVM
    {
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }

        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
    }
}
