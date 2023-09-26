using MusicConcert.Data;
using MusicConcert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicConcert.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _context = null;
        public EventRepository(EventContext context)
        {
            _context = context;
        }

        //public void AddNewEvent(EventViewModel model)
        public async Task<int> AddNewEvent(EventViewModel model)
        {
            var newEvent = new Events()
            {
                Title = model.title,
                Date = model.date,
                StartTime = model.startTime,
                Location = model.location,
                Description = model.description,
                Duration = model.duration,
                Organiser = model.organiser,
                TypeOfEvent = model.eventType,
                DateCreatedOn = DateTime.Now,
                OtherDetails = model.otherDetails,
                // Invitees = model.invitees   // was voilating 1NF

            };
            await _context.Events.AddAsync(newEvent);
            // _context.SaveChanges();
            await _context.SaveChangesAsync();

            // loop through all the Invitees
            if (model.invitees != null)
            {
                string[] inviteeList = model.invitees.Split(',');
                foreach (string person in inviteeList)
                {
                    var newInvitees = new Invitees()
                    {
                        EventId = newEvent.Id,
                        InviteeName = person,
                        DateAdded = DateTime.Now,

                    };
                    _context.Invitees.Add(newInvitees);
                    _context.SaveChanges();
                }
            }
            return newEvent.Id;


            // if have to return then - return newEvent.Id;
        }

        public async Task<List<EventViewModel>> GetAllEvents()
        {
            var events = new List<EventViewModel>();
            var allPublicEvents = await _context.Events.Where(e => e.TypeOfEvent == "Public").ToListAsync();

            if (allPublicEvents?.Any() == true)
            {
                foreach (var evnt in allPublicEvents)
                {
                    events.Add(new EventViewModel()
                    {
                        Id = evnt.Id,
                        title = evnt.Title,
                        date = evnt.Date.Value,
                        startTime = evnt.StartTime.Value,
                        location = evnt.Location,
                        description = evnt.Description,
                        duration = evnt.Duration,
                        organiser = evnt.Organiser,

                    });
                }
            }

            return events;
        }

        // functionality for MyEvents
        public async Task<List<EventViewModel>> GetEventByOraganiserName(string organiser)
        {
            var eventsByOrganiser = new List<EventViewModel>();
            var allEvents = await _context.Events.Where(o => o.Organiser == organiser).ToListAsync();
            if (allEvents?.Any() == true)
            {
                foreach (var evnt in allEvents)
                {
                    eventsByOrganiser.Add(new EventViewModel()
                    {
                        Id = evnt.Id,
                        title = evnt.Title,
                        date = evnt.Date.Value,
                        startTime = evnt.StartTime.Value,
                        location = evnt.Location,
                        description = evnt.Description,
                        duration = evnt.Duration,
                        organiser = evnt.Organiser,

                    });
                }

            }
            return eventsByOrganiser;

        }

        public async Task<EventViewModel> GetEventById(int id)
        {
            var evnt = await _context.Events.FindAsync(id);
            if (evnt != null)
            {
                var eventDetails = new EventViewModel()
                {
                    Id = evnt.Id, // newly added
                    title = evnt.Title,
                    date = evnt.Date.Value,
                    startTime = evnt.StartTime.Value,
                    location = evnt.Location,
                    description = evnt.Description,
                    duration = evnt.Duration,
                    organiser = evnt.Organiser,
                    invitees = evnt.Invitees,   // required for updating
                };
                return eventDetails;
            }
            return null;

        }
        public async Task<int> InviteesCountOfAnEvent(int id)
        {
            int totalInvitees = await _context.Invitees.CountAsync(e => e.EventId == id);
            return totalInvitees;

        }
        // for comments by id
        public async Task<List<Comments>> GetComments(int id)
        {
            var comments = new List<Comments>();
            var allComments = await _context.Comments.Where(e => e.EventId == id).ToListAsync();
            if (allComments.Any() == true)
            {
                foreach (var text in allComments)
                {
                    comments.Add(new Comments()
                    {
                        Id = text.Id,
                        comment = text.comment
                    });
                }
            }
            return comments;
        }

        public async Task<int> AddNewComment(int eventId, string commentText)
        {
            // var comment = new CommentViewModel();
            var newComment = new Comments()
            {
                EventId = eventId,
                comment = commentText,
                DateAdded = DateTime.Now

            };
            await _context.Comments.AddAsync(newComment);
            // _context.SaveChanges();
            await _context.SaveChangesAsync();
            return newComment.Id;
        }


        // Event invited to
        public async Task<List<EventViewModel>> EventInvitedTo(string userEmail) //like  abhi@gmail.com
        {
            var events = new List<EventViewModel>();
            //var AllInvititions = await _context.Events.Where(e => e.Invitees.Contains(userName)).ToListAsync();

            var AllInvitations = await _context.Events
                .Where(e => _context.Invitees.Where(i => i.InviteeName == userEmail)
                .Select(i => i.EventId)
                .Contains(e.Id))
                .ToListAsync();


            if (AllInvitations?.Any() == true)
            {
                foreach (var evnt in AllInvitations)
                {
                    events.Add(new EventViewModel()
                    {
                        Id = evnt.Id,
                        title = evnt.Title,
                        date = evnt.Date.Value,
                        startTime = evnt.StartTime.Value,
                        location = evnt.Location,
                        description = evnt.Description,
                        duration = evnt.Duration,
                        organiser = evnt.Organiser,

                    });
                }
            }

            return events;
        }

        public async Task<string> GetInviteesList(int id)
        {
            //var invitees = new List<string>();
            var invitees = string.Empty;
            var inviteesList = await _context.Invitees.Where(e => e.EventId == id).ToArrayAsync();
            if (inviteesList != null)
            {
                for (int i = 0; i < inviteesList.Length; i++)
                {
                    invitees += $"{inviteesList[i].InviteeName}";

                    if (i < inviteesList.Length - 1)
                    {
                        invitees += ",";
                    }
                }
                return invitees;

            }
            return null;
        }

        public async Task<int> UpdateEvent(int id, EventViewModel updatedModel)
        {

            var eventToUpdate = await _context.Events.FindAsync(id);
            if (eventToUpdate == null)
            {
                return -1;
            }
            else
            {
                eventToUpdate.Title = updatedModel.title;
                eventToUpdate.Date = updatedModel.date;
                eventToUpdate.StartTime = updatedModel.startTime;
                eventToUpdate.Location = updatedModel.location;
                eventToUpdate.Description = updatedModel.description;
                eventToUpdate.Duration = updatedModel.duration;
                eventToUpdate.Organiser = updatedModel.organiser;
                eventToUpdate.TypeOfEvent = updatedModel.eventType;
                eventToUpdate.DateUpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();
                return 1;
            }

        }

        // Get all Events for Admin view
        public async Task<List<EventViewModel>> GetAllEventsForAdmin()
        {
            var events = new List<EventViewModel>();
            var allEvents = await _context.Events.ToListAsync();
            if (allEvents?.Any() == true)
            {
                foreach (var evnt in allEvents)
                {
                    events.Add(new EventViewModel()
                    {
                        Id = evnt.Id,
                        title = evnt.Title,
                        date = evnt.Date.Value,
                        startTime = evnt.StartTime.Value,
                        location = evnt.Location,
                        description = evnt.Description,
                        duration = evnt.Duration,
                        organiser = evnt.Organiser,
                    });
                }
            }
            return events;
        }

    }// eventRepo
}
