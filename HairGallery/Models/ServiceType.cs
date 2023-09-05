﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HairGallery.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }
        [Display(Name = "Service Type")]
        public string Type { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}