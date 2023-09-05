using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HairGallery.Models
{
    public class Owner
    {
        [Key]
        public int ownerID { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 228, ErrorMessage = "First Name must be atleast 3 characters long", MinimumLength = 3)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 228, ErrorMessage = "Last Name must be atleast 3 characters long", MinimumLength = 3)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Identity Number")]
        [MaxLength(13), MinLength(13)]
        public string IDNumber { get; set; }
        public string Status { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Alternative Number")]
        public string AltContactNumber { get; set; }
    }
}