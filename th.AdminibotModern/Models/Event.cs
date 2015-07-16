using System;
using System.Collections.ObjectModel;

namespace th.AdminibotModern.Models
{
    public class Event
    {
        public int EventLevel { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDescription { get; set; }

        public Event(int eventLevel, DateTime eventDate, string eventDescription)
        {
            this.EventLevel = eventLevel;
            this.EventDate = eventDate;
            this.EventDescription = eventDescription;
        }
    }

    public class Events : ObservableCollection<Event>
    {
        private static object _threadLock = new Object();
        private static Events _current = null;

        public static Events Current
        {
            get
            {
                lock (_threadLock)
                if (_current == null) _current = new Events();
                return _current;
            }
        }

        public void AddEvent(int eventLevel, DateTime eventDate, string eventDescription)
        {
            Event addedEvent = new Event(eventLevel, eventDate, eventDescription);
            Add(addedEvent);
        }
    }
}
