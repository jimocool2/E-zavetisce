using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{

    public class Pet
    {
        public int PetID { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]
        public string? Name { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Column("Type")]
        [StringLength(50)]
        public string? Type { get; set; }
        // Za dodat slike, more se se pogledat kako
    }
}

