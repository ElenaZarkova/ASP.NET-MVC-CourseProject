﻿using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
