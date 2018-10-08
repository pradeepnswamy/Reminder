using System;
using System.Collections.Generic;
using System.Text;
using Reminder.Model;

namespace Reminder.PlatformDependent
{
    public interface IEventKitHandler
    {
        void CreateService();
        void CreateReminder();
        void SaveEvent(EventModel eventModel);

    }
}
