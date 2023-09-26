using MusicConcert.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Data
{
    public class EventContext : IdentityDbContext<ApplicationUser>
    {
        public EventContext(DbContextOptions<EventContext> options)
            :base(options)
        {

        }
        // entity class
        public DbSet<Events> Events { get; set; } // name of table: Events
        public DbSet<Invitees> Invitees { get; set; } // new table Invitees
        public DbSet<Comments> Comments { get; set; } // new table for comments
    }
}
