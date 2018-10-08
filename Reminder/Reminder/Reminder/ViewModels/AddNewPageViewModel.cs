using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using Reminder.PlatformDependent;
using System.Diagnostics;
using System.Threading.Tasks;
using Reminder.Model;
using System;

namespace Reminder.ViewModels
{
	public class AddNewPageViewModel : BindableBase
	{
        private string _eventTypeLabel;
        public string EventTypeLabel
        {
            get { return _eventTypeLabel; }
            set { SetProperty(ref _eventTypeLabel, value); }
        }

        private string _contactNameLabel;
        public string ContactNameLabel
        {
            get { return _contactNameLabel; }
            set { SetProperty(ref _contactNameLabel, value); }
        }

        private string _relationshipLabel;
        public string RelationshipLabel
        {
            get { return _relationshipLabel; }
            set { SetProperty(ref _relationshipLabel, value); }
        }

        private string _eventDateLabel;
        public string EventDateLabel
        {
            get { return _eventDateLabel; }
            set { SetProperty(ref _eventDateLabel, value); }
        }

        private string _remindLabel;
        public string RemindLabel
        {
            get { return _remindLabel; }
            set { SetProperty(ref _remindLabel, value); }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetProperty(ref _selectedDate, value); }
        }

        IPageDialogService _dialogService;
        IDependencyService _dependencyService;
        public DelegateCommand EventTypeCommand { get; set; }
        public DelegateCommand AddContactCommand { get; set; }
        public DelegateCommand RelationshipCommand { get; set; }
        public DelegateCommand EventDateCommand { get; set; }
        public DelegateCommand RemindCommand { get; set; }

        public DelegateCommand SaveReminder { get; set; }
        public AddNewPageViewModel(IPageDialogService dialogService, IDependencyService dependencyService)
        {
            _dialogService = dialogService;
            _dependencyService = dependencyService;
            EventTypeLabel = "Select the Event Type*";
            ContactNameLabel = "Add Contact*";
            RelationshipLabel = "Select RelationShip";
            EventDateLabel = "Select Event Date*";
            RemindLabel = "Remind Before*";
            SelectedDate = DateTime.Today;
            EventTypeCommand = new DelegateCommand(async ()=> await selectEventTypeAsync());
            AddContactCommand = new DelegateCommand(async() => await selectAddContactAsync());
            RelationshipCommand = new DelegateCommand(async () => await selectRelationshipAsync());
            EventDateCommand = new DelegateCommand(async () => await selectEventDateAsync());
            RemindCommand = new DelegateCommand(async () => await selectRemindBeforeAsync());
            SaveReminder = new DelegateCommand(saveReminder);
        }

        private async Task selectEventTypeAsync()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Event Type", "Cancel", null, "Birthday", "Wedding Anniversary", "Job Anniversary", "Others/Custom");
            Debug.WriteLine("Action: " + action);
            EventTypeLabel = action.ToString();
        }
        private async Task selectAddContactAsync()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Add Contact", "Cancel", null, "Sync Contacts", "New Contact");
            Debug.WriteLine("Action: " + action);
            ContactNameLabel = action.ToString();
        }
        private async Task selectRelationshipAsync()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Relationship", "Cancel", null, "Family", "Friends", "Colleague", "Other-Specify");
            Debug.WriteLine("Action: " + action);
            RelationshipLabel = action.ToString();
        }
        private async Task selectEventDateAsync()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Need to show the date picker", "Cancel", null, "Today", "Tomorrow");
            Debug.WriteLine("Action: " + action);
            EventDateLabel = action.ToString();
        }
        private async Task selectRemindBeforeAsync()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Remind Before", "Cancel", null, "1 Day", "1 Week", "Custom - Specify Days");
            Debug.WriteLine("Action: " + action);
            RemindLabel = action.ToString();
        }
        public void saveReminder()
        {
            EventModel em = PrepareEventModel();
            _dependencyService.Get<IEventKitHandler>().SaveEvent(em);
            if (validateForm())
            {
                _dialogService.DisplayAlertAsync("Reminder Saved", null, "OK");


            }
            else
                _dialogService.DisplayAlertAsync("Please fill all Mandatory Fields", null, "OK");
        }

        private EventModel PrepareEventModel()
        {
            EventModel em = new EventModel();
            em.Contact = ContactNameLabel;
            em.EventDate = SelectedDate;
            em.EventType = EventTypeLabel;
            em.Relationship = RelationshipLabel;
            em.RemindBefore = 1;//RemindLabel
            return em;
        }
        private bool validateForm()
        {
            if (EventTypeLabel != "Select the Event Type*" && ContactNameLabel != "Add Contact*" && 
                EventDateLabel != "Select Event Date*" && RemindLabel != "Remind Before*")
                return true;
            return false;
        }
    }
}
