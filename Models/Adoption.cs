using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class Adoption
    {
        public int PetID { get; set; }
        public string ClientID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Adopted")]
        public DateTime DateAdopted { get; set; }

        public Pet? Pet { get; set; }
        public Client? Client { get; set; }
    }
}