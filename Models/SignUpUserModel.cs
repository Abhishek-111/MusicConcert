using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MusicConcert.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter Your Full Name")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }


        [Required(ErrorMessage="Please Enter Your Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter Your Password")]
        [Compare("CnfPassword",ErrorMessage ="Password Donot Match!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Re-enter Your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string CnfPassword { get; set; }

    }
}
