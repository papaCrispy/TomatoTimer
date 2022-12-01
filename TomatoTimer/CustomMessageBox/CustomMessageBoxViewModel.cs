using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TomatoTimer.CustomMessageBox
{
    public class CustomMessageBoxViewModel
    {

        private string _windowMessage;
        private string _windowTitle;

        public string WindowMessage
        {
            get { return _windowMessage; }
            set { _windowMessage = value; }
        }

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }

        public UserActionsCommands ExecuteUserChoice { get; set; }
        public delegate void MessageBoxReturnedEventHandler(object sender, UserChoice userChoice);
        public event MessageBoxReturnedEventHandler UserMadeChoice;

        public CustomMessageBoxViewModel()
        {
            ExecuteUserChoice = new UserActionsCommands(OnExecuteUserChoice);
        }

        public void OnExecuteUserChoice(object parameter)
        {
            switch(parameter as string)
            {
                case "Ok":
                    UserMadeChoice.Invoke(this, UserChoice.OK);
                    break;
                case "Cancel":
                    UserMadeChoice.Invoke(this, UserChoice.Cancel);
                    break;
                default:
                    break;
            }
        }


    }
}
