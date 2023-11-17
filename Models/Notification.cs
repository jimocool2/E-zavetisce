using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Body { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        public Employee? Employee { get; set; }

        // Mogoce dodaja se kaksne slike

    }
}