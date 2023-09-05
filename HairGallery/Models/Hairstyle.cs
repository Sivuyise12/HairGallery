using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace HairGallery.Models
{
    public class Hairstyle
    {
        [Key]
        public int HairstyleId { get; set; }
        public int SalonBuildingId { get; set; }
        public int ServiceTypeId { get; set; }
        [DisplayName("Hairstyle Name")]
        public string HairstyleName { get; set; }

        [DisplayName("Hairstyle Description")]
        public string HairstyleDescription { get; set; }
        public byte[] HairstylePicture { get; set; }
        [DisplayName("Hairstyle Price"), DataType(DataType.Currency)]
        public decimal HairstylePrice { get; set; }
        public string CreatedBy { get; set; }
        public virtual SalonBuilding SalonBuildings { get; set; }
        public virtual ServiceType ServiceTypes { get; set; }
    }
}