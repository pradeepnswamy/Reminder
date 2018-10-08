using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventKit;
using Foundation;
using Reminder.iOS.PlatfromDependent;
using Reminder.PlatformDependent;
using Reminder.Model;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(EventKitHandler))]
namespace Reminder.iOS.PlatfromDependent
{
    public class EventKitHandler: IEventKitHandler
    {
        protected EKEventStore eventStore;
        public EKEventStore EventStore
        {
            get { return eventStore; }
        }

        private bool accessGranted = false;

        public void CreateService()
        {

            /*if(EventStore == null)
            {
                eventStore = new EKEventStore();
                try
                {
                    eventStore.RequestAccess(EKEntityType.Event, (bool granted, NSError e) => {
                        if (granted)
                        {
                            accessGranted = true;
                        }
                        else
                        {
                            new UIAlertView("Access Denied", "User Denied Access to Calendar Data", null, "ok", null).Show();
                            accessGranted = false;
                        }
                    });
                }
                catch(Exception ex)
                {
                    var e = ex.Message;
                }
            }*/
        }

        public void SaveEvent(EventModel eventModel)
        {
            var notification = new UILocalNotification();


            // set the fire date (the date time in which it will fire)
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(120);

            // configure the alert
            notification.AlertAction = eventModel.EventType;
            notification.AlertBody = eventModel.Relationship;

            // modify the badge
            notification.ApplicationIconBadgeNumber = 1;

            // set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            // schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        /*public void SaveEvent()
        {
            EKEvent newEvent = EKEvent.FromStore(eventStore);
            // set the alarm for 5 minutes from now
            newEvent.AddAlarm(EKAlarm.FromDate((NSDate)DateTime.Now.AddMinutes(1)));
            // make the event start 10 minutes from now and last 30 minutes
            newEvent.StartDate = (NSDate)DateTime.Now.AddMinutes(4);
            newEvent.EndDate = (NSDate)DateTime.Now.AddMinutes(8);
            newEvent.Title = "Appt. to do something Awesome!";
            newEvent.Notes = "Find a boulder, climb it. Find a river, swim it. Find an ocean, dive it.";
            newEvent.Calendar = eventStore.DefaultCalendarForNewEvents;

            // save the event
            NSError e;
            eventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out e);
            if (e != null)
            {
                new UIAlertView("Err Saving Event", e.ToString(), null, "ok", null).Show();
                return;
            }
            else
            {
                new UIAlertView("Event Saved", "Event ID: " + newEvent.EventIdentifier, null, "ok", null).Show();
                Console.WriteLine("Event Saved, ID: " + newEvent.EventIdentifier);
            }

            // to retrieve the event you can call
            EKEvent mySavedEvent = eventStore.EventFromIdentifier(newEvent.EventIdentifier);
            Console.WriteLine("Retrieved Saved Event: " + mySavedEvent.Title);
            /-*
            // to delete, note that once you remove the event, the reference will be null, so
            // if you try to access it you'll get a null reference error.
            eventStore.RemoveEvent(mySavedEvent, EKSpan.ThisEvent, true, out e);
            Console.WriteLine("Event Deleted.");*-/

        }*/

        public void CreateReminder()
        {
            EKReminder reminder = EKReminder.Create(eventStore);
            reminder.Title = "first reminder";
            reminder.Calendar = eventStore.DefaultCalendarForNewReminders;
            NSError e;
            eventStore.SaveReminder(reminder, true, out e);
            if (e != null)
            {
                new UIAlertView("err saving reminder", e.ToString(), null, "ok", null).Show();
                return;
            }
            else
            {
                new UIAlertView("reminder saved", "ID: " + reminder.CalendarItemIdentifier, null, "ok", null).Show();
            }
            /*
            //to retrieve the reminders
            EKCalendarItem myReminder = eventStore.GetCalendarItem(reminder.CalendarItemIdentifier);
            Console.WriteLine("Retrieved Saved Reminder: " + myReminder.Title);

            //to delete the reminders
            eventStore.RemoveReminder(myReminder as EKReminder, true, out e);
            Console.WriteLine("Reminder Deleted.");*/
        }

        /*protected void GetRemindersViaQuery()
        {
            // create our NSPredicate which we'll use for the query
            NSPredicate query = eventStore.PredicateForReminders(null);

            // execute the query
            eventStore.FetchReminders(
                query, (EKReminder[] items) => {
                    // since this is happening in a completion callback, we have to update
                    // on the main thread
                        // create a new event list screen with these events and show it
                        eventListScreen = new Calendars.Screens.EventList.EventListController(items, EKEntityType.Reminder);
                        NavigationController.PushViewController(eventListScreen, true);
                });
        }*/
    }
}