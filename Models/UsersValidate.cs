using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class UsersValidate
    {
        [Required(ErrorMessage = "First Name is required for registration")]
        [MinLength(2, ErrorMessage = "First Name field should be at least 2 characters long")]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "Names cannot be numbers")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Last Name is required for registration")]
        [MinLength(2, ErrorMessage = "Last Name field should be at least 2 characters long")]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "Names cannot be numbers")]
        public string last_name { get; set; }
        [Required(ErrorMessage = "Email Address is required for registration")]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required for registration")]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The passwords do not match.")]
        public string passconfirm { get; set; }
    }
}