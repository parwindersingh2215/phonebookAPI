﻿using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Models
{
    public class UserContactInputModel
    {

     
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string LandLineNo { get; set; }
        public string? AlternateMobileNo { get; set; }

    }
}
