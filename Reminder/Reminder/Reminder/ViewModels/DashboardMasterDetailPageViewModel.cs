using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.ViewModels
{
	public class DashboardMasterDetailPageViewModel : BindableBase
	{
        INavigationService _navigationService;
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DashboardMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateTo);
        }

        private void NavigateTo(string page)
        {
            _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}
