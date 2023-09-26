using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Data
{
    public class Events
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; } // 
        public string Organiser { get; set; }
        public string TypeOfEvent { get; set; }
        public string OtherDetails { get; set; }
        public string Invitees { get; set; }
        public DateTime? DateCreatedOn { get; set; }
        public DateTime? DateUpdatedOn { get; set; }
    }
}
