using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_zavetisce.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        [Display(Name = "Naslov")]
        public string Title { get; set; }
        [Display(Name = "Vsebina")]
        public string Body { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Objavljeno")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Avtor")]
        public Employee? Employee { get; set; }
        public string? EmployeeID { get; set; }
    }
}