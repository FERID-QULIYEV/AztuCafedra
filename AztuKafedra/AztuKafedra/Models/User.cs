﻿using AztuKafedra.Models.BaseId;
using System.ComponentModel.DataAnnotations;

namespace AztuKafedra.Models
{
    public class User:Base
    { 
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

         public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
        // PasitionId için dış anahtar, CASCADE silme yolu kaldırıldı
    
    }
}
