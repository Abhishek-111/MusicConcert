using MusicConcert.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository = null;
        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
       

        public async Task<ViewResult> Index()  // getting all events on home page
        {
            var data = await _eventRepository.GetAllEvents();
            
            return View(data); //send data to view page
        }

        
    }
}
