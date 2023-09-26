using MusicConcert.Data;
using MusicConcert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicConcert.Repository
{
    public interface IEventRepository
    {
        Task<int> AddNewEvent(EventViewModel model);
        Task<List<EventViewModel>> EventInvitedTo(string userEmail);
        Task<List<EventViewModel>> GetAllEvents();
        Task<List<EventViewModel>> GetAllEventsForAdmin();
        Task<EventViewModel> GetEventById(int id);
        Task<List<EventViewModel>> GetEventByOraganiserName(string organiser);
        Task<string> GetInviteesList(int id);

        Task<int> InviteesCountOfAnEvent(int id);
        Task<int> UpdateEvent(int id, EventViewModel updatedModel);

        Task<int> AddNewComment(int eventId, string commentText);
        Task<List<Comments>> GetComments(int id);
    }
}