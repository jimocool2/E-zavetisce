using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class Adoption
    {
        public int AdoptionID { get; set; }

        [Required]
        public Pet? Pet { get; set; }

        [Required]
        public Client? Client { get; set; }
        // Kdo je odobirl adoption
        [Required]
        public Employee? Employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }
}