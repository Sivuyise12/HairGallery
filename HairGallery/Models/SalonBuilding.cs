using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HairGallery.Models
{
    public class SalonBuilding
    {
        [Key]
        public int SalonBuildingId { get; set; }
        [DisplayName("Owner Email")]
        public string OwnerEmail { get; set; }
        [DisplayName("Salon Name")]
        public string SalonName { get; set; }
        public string Status { get; set; }
        [AllowHtml]
        [Display(Name = "Building Description")]
        public string BuildingDescription { get; set; }
        [Display(Name = "Picture")]
        public byte[] BuildingPic { get; set; }
        public string Address { get; set; }
        public ICollection<Hairstyle> Hairstyles { get; set; }
        public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}