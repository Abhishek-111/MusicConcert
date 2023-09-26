using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Data
{
    public class Invitees
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        public string InviteeName { get; set; }
        public DateTime? DateAdded { get; set; }
       
    }
}
