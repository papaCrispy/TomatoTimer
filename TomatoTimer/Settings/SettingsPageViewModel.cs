using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TomatoTimer.Commands;
using TomatoTimer.Databases;

namespace TomatoTimer.Settings
{
    public class SettingsPageViewModel : BaseViewModel
    {

        private AppSettingsDatabase _databaseManager;
        private List<string> _timerSettings;

        public delegate void SettingsChanged();
        public event SettingsChanged SettingsChangedEvent;

        private string _pomodoroMinutes;
        public string PomodoroMinutes
        {
            get 
            { return _pomodoroMinutes; }
            set 
            { if(!CheckStringPropertness(value))
                {
                    return;
                }
                _pomodoroMinutes = value;
            }
        }
        
        private string _shortBreakMinutes;
        public string ShortBreakMinutes
        {
            get 
            { return _shortBreakMinutes; }
            set
            {
                if (!CheckStringPropertness(value))
                {
                    return;
                }
                _shortBreakMinutes = value;
            }
        }
        
        private string _longBreakMinutes;
        public string LongBreakMinutes
        {
            get 
            { return _longBreakMinutes; }
            set
            {
                if (!CheckStringPropertness(value))
                {
                    return;
                }
                _longBreakMinutes = value;
            }
        }

        public RelayCommand SaveSettings { get; set; }

        public SettingsPageViewModel()
        {
            _databaseManager = new AppSettingsDatabase();
            _timerSettings = new List<string>(_databaseManager.ReadDataFromTheDatabase());

            PomodoroMinutes = (int.Parse(_timerSettings.ElementAt(0)) / 60).ToString();
            ShortBreakMinutes = (int.Parse(_timerSettings.ElementAt(1)) / 60).ToString();
            LongBreakMinutes = (int.Parse(_timerSettings.ElementAt(2)) / 60).ToString();

            SaveSettings = new RelayCommand(OnSaveSettings);
        }

        private bool CheckStringPropertness(string stringToCheck)
        {
            foreach(char sign in stringToCheck)
            {
                if(!char.IsDigit(sign))
                {
                    return false;
                }
            }

            return true;
        }

        private void OnSaveSettings(object sender)
        {
            _databaseManager.UpdateTheDatabase("Pomodoro", int.Parse(PomodoroMinutes));
            _databaseManager.UpdateTheDatabase("ShortBreak", int.Parse(ShortBreakMinutes));
            _databaseManager.UpdateTheDatabase("LongBreak", int.Parse(LongBreakMinutes));

            SettingsChangedEvent.Invoke();

        }



    }
}
