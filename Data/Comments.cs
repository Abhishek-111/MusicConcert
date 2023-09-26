using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Data
{
    public class Comments
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        public string comment { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
