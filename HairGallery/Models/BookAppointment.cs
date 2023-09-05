using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HairGallery.Models
{
    public class BookAppointment
    {
        [Key]
        public int BookAppointmentId { get; set; }
        [Display(Name = "Service Type")]
        public string serviceType { get; set; }
        [Display(Name = "Date Requesting For")]
        [DataType(DataType.Date)]
        public string DateRequestingFor { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Hairstyle Price")]
        public decimal HairstylePrice { get; set; }
        public string Description { get; set; }
        public string HairstyleName { get; set; }
        public string SalonName { get; set; }
        public string OwnerEmail { get; set; }
        public string CustomerEmail { get; set; }
        [Display(Name = "Date Requested")]
        public string DateRequested { get; set; }
        [Display(Name = "Time Slot")]
        [DataType(DataType.Time)]
        public string TimeSlot { get; set; }
    }
}