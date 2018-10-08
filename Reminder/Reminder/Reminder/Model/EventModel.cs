using System;
namespace Reminder.Model
{
    public class EventModel
    {
        public string EventType { get; set; }
        public string Contact { get; set; }
        public string Relationship { get; set; }
        public DateTime EventDate { get; set; }
        public int RemindBefore { get; set; }
    }
}
