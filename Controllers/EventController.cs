using MusicConcert.Models;
using MusicConcert.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;  
using MusicConcert.Data;
using Microsoft.AspNetCore.Authorization;

namespace MusicConcert.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository = null;


        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IEventRepository eventRepository, UserManager<ApplicationUser> userManager) // dependency injection
        {
            _eventRepository = eventRepository;
            // for loggedi n user
            _userManager = userManager;
        }

        [Route("Events")]
        public IActionResult Index()
        {
            return View();
        }

        //[Route("CreateEvent")]
        [Authorize]
        public ViewResult AddNewEvent(bool isSuccess = false, int eventId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.EventId = eventId;
            return View();
        }

        //[Route("CreateEvent")]
        [HttpPost]
        public async Task<IActionResult> AddNewEvent(EventViewModel eventModel)
        {
            ViewData["User"] = "abhi";
            ViewBag.User = "abhi";
            if (ModelState.IsValid)
            {
                int id = await _eventRepository.AddNewEvent(eventModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewEvent), new { isSuccess = true, eventId = id });
                }

            }


            return View();
        }





        [Authorize]
        public async Task<ViewResult> GetEventByOraganiserName()
        {
            // get current logged in user 
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ViewData["User"] = user.Email;
                var data = await _eventRepository.GetEventByOraganiserName(user.Email);
                return View(data);
            }

            return View();
        }

        // Display an individual event by id
        public async Task<ViewResult> GetEvent(int id)
        {
            var data = await _eventRepository.GetEventById(id);

            return View(data);
        }

        //GetEvent on Post
        [HttpPost]
        //[Route("getevent/{comment}")]
        public async Task<ActionResult> AddComment(int id, string comment)
        {
            

            int Id = await _eventRepository.AddNewComment(id, comment);
            if (Id > 0)
            {
                return RedirectToAction("GetEvent", new { id = id });
            }
           // return View(data);
            return null;
           
        }

        // for InvitedToEvents

        [Authorize]
        public async Task<ViewResult> EventInvitedTo()
        {
            // get current logged in user 
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                //ViewData["User"] = user.Email;
                string userEmail = user.Email;

                var data = await _eventRepository.EventInvitedTo(userEmail);
                return View(data);
            }

            return View();
        }

        // Update Event

        [Authorize]
        public async Task<ViewResult> GetEventForUpdate(int id, bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            var data = await _eventRepository.GetEventById(id);
            var eventList = await _eventRepository.GetInviteesList(id);

            ViewData["Invitees"] = eventList;

            return View(data);

        }


        [HttpPost]
        public async Task<IActionResult> GetEventForUpdate(EventViewModel eventModel)
        {


            if (ModelState.IsValid)
            {
                int id = await _eventRepository.UpdateEvent(eventModel.Id, eventModel);
                if (id == 1)
                {
                    return RedirectToAction(nameof(GetEventForUpdate), new { isSuccess = true });
                }
            }
            // ViewData["Invitees"] = eventList;

            return View();

        }

        [Authorize]
        public async Task<ViewResult> GetAllEventsForAdmin()
        {
            var data = await _eventRepository.GetAllEventsForAdmin();
            return View(data);

        }


    }
}
