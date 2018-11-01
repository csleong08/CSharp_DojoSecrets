using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class LoginValidate
    {
        [Required(ErrorMessage = "Email Address is required to login")]
        [EmailAddress(ErrorMessage = "Email Address is not registered")]
        public string login_email { get; set; }
        [Required(ErrorMessage = "Password is required to login")]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        // [Compare("password", ErrorMessage = "The passwords do not match.")]
        public string login_password { get; set; }
    }
}