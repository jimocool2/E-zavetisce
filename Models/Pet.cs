using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;

namespace E_zavetisce.Models
{

    public class Pet
    {
        public int PetID { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]
        [Display(Name = "Ime")]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Predan")]
        public DateTime DateAdded { get; set; }

        [Column("Type")]
        [Display(Name = "Tip in opis")]
        [StringLength(500)]
        public string Type { get; set; }

        public bool Adopted { get; set; }
    }
}

