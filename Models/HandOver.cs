using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class HandOver
    {
        public int HandOverID { get; set; }
        public string ClientID { get; set; }
        public int PetID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum predaje")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Stranka")]
        public Client Client { get; set; }
        [Display(Name = "Žival")]
        public Pet Pet { get; set; }
    }
}