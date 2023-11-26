using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class Client
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ClientID { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "Ime")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [Required]
        [Column("LastName")]
        [Display(Name = "Priimek")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum pridružitve")]
        public DateTime DateJoined { get; set; }

        [Display(Name = "Ime")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<Adoption>? Adoptions { get; set; }
        public ICollection<HandOver>? HandOvers { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
