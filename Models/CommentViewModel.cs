using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Models
{
    public class CommentViewModel
    {
        //public int id { get; set; }

        //[Required(ErrorMessage = "Please enter Title of Book")]
        [Display(Name = "Comments Here..")]
        public string comment { get; set; }
    }
}
