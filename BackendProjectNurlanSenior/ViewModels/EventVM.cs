using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class EventVM
    {
        public List<Speaker> Speakers { get; set; }
        public List<Event> Events { get; set; }

        public List<EventSpeaker> EventSpeakers { get; set; }

        public Event Event { get; set; }
    }
}
