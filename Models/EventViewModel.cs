using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter Title of Event")]
        [Display(Name = "Title of the Event")]
        public string title { get; set; }

        [Required(ErrorMessage = "Please enter Event Date")]
        [Display(Name = "Event Date")]
       
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Please enter start time")]
        [Display(Name = "Start Time")]
        //[DataType(DataType.Date)]
        [DataType(DataType.Time)] 
       
        public DateTime startTime { get; set; }

        [Required(ErrorMessage = "Please enter Venue")]
        [Display(Name = "Venue")]
        public string location { get; set; }

       // [Required(ErrorMessage = "Description")]
        [Display(Name = "Description")]
        public string description { get; set; }

        
        [Display(Name = "Other Details")]
        public string otherDetails { get; set; }

        // [Range(1,4, ErrorMessage ="Duration should be 1-4 hr only")]
        [Display(Name = "Duration")]
        public string /*int?*/ duration { get; set; }

        [Display(Name = "Event Organiser")]
       // [Required(ErrorMessage = "Please enter Your Name")]
        public string organiser { get; set; }

        [Display(Name ="Type of Event")]
        //[Required(ErrorMessage ="Please Choose Type of Event")]
        public string eventType { get; set; }

        [Display(Name = "Invite People")]
        public string invitees { get; set; }
        //public string comments { get; set; }
    }
}
