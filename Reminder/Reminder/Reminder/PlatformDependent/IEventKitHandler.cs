using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.PlatformDependent
{
    public interface IEventKitHandler
    {
        void CreateService();
        void CreateReminder();
        void SaveEvent();

    }
}
