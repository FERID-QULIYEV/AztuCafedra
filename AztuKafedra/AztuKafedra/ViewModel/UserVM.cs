
using System.ComponentModel.DataAnnotations;

namespace AztuKafedra.ViewModel
{
    public class UserVM
    {
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }
        public IFormFile Photo { get; set; }
        public string Description { get; set; }

        public int ChildCategoryId { get; set; }
    }
}
