using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.ViewModels
{
	public class RegistrationPageViewModel : BindableBase
	{
        public DelegateCommand SignUpCommand { get; set; }
        public RegistrationPageViewModel()
        {
            SignUpCommand = new DelegateCommand(SignUp);
        }

        private void SignUp()
        {
            if(ValidateForm())
            {
                
            }
        }

        private bool ValidateForm()
        {
            return false;
        }
    }
}
