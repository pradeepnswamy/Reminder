using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reminder.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public DelegateCommand NavigateToRegistraionPage { get; set; }
        public DelegateCommand NavigateToDashboardPage { get; set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService; 
            NavigateToRegistraionPage = new DelegateCommand(NavigateToRegistraion);
            NavigateToDashboardPage = new DelegateCommand(NavigateToDashboard);
        }

        private void NavigateToDashboard()
        {
            _navigationService.NavigateAsync("/DashboardMasterDetailPage/NavigationPage/AddNewPage");
            //_navigationService.NavigateAsync("NavigationPage/DashboardPage");
        }

        private void NavigateToRegistraion()
        {
            _navigationService.NavigateAsync("NavigationPage/RegistrationPage");
        }
    }
}