using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TomatoTimer.Databases;
using TomatoTimer.CustomMessageBox;

namespace TomatoTimer.Timer
{
    public enum TimerMode
    {
        Pomodoro,
        ShortBreak,
        LongBreak
    }

    public class TimerPageViewModel : BaseViewModel
    {
        private TaskTimer _taskTimer;

        private AppSettingsDatabase _databaseManager;

        private string _taskTimeLeft;
        public string TaskTimeLeft
        {
            get { return _taskTimeLeft; }
            set { _taskTimeLeft = value;
                  OnPropertyRaised("TaskTimeLeft"); }
        }

        private TimerMode _timerMode;
        public TimerMode TimerMode
        {
            get 
            { return _timerMode; }
            set 
            { 
                if (_timerMode == value)
                {
                    return;
                }
                _timerMode = value;
                UpdateTimerDuration();
            }
        }

        #region Commands
            
        public TimerCommand ChangeTimerState
        {
            get;
            private set;
        }

        public TimerCommand ChangeTimerMode
        {
            get;
            private set;
        }

            
        #endregion

        public TimerPageViewModel()
        {

            TimerMode = TimerMode.Pomodoro;
            _databaseManager = new AppSettingsDatabase();

            _taskTimer = new TaskTimer();
            UpdateTimerDuration();
            _taskTimeLeft = String.Format("{0:D2}:{1:D2}", TimeSpan.FromSeconds(_taskTimer.Duration).Minutes, TimeSpan.FromSeconds(_taskTimer.Duration).Seconds);
            _taskTimer.DispatcherTimer.Tick += UpdateTaskTimeLeft;

            ChangeTimerState = new TimerCommand(OnChangeTimerState);
            ChangeTimerMode = new TimerCommand(OnChangeTimerMode);
        }

        private void UpdateTaskTimeLeft(object sender, EventArgs e)
        {

            if (!(_taskTimer.Duration != 0 && _taskTimer.Duration > 0))
            {
                _taskTimer.DispatcherTimer.Stop();
                SoundPlayer mediaPlayer = new SoundPlayer(@"..\..\..\Assets\Sounds\clock-alarm-8761.wav");
                mediaPlayer.Play();
            }
            else
            {
                _taskTimer.Duration -= 1;
            }
            TaskTimeLeft = String.Format("{0:D2}:{1:D2}", TimeSpan.FromSeconds(_taskTimer.Duration).Minutes, TimeSpan.FromSeconds(_taskTimer.Duration).Seconds);
        }

        private void OnChangeTimerState(object property)
        {
            string commandParameter = property.ToString();

            switch(commandParameter)
            {
                case "Start":
                    if(_taskTimer.Duration == 0)
                    {
                        UpdateTimerDuration();
                    }
                    _taskTimer.DispatcherTimer.Start();
                    break;
                case "Stop":
                    _taskTimer.DispatcherTimer.Stop();
                    break;

            }
        }

        private void OnChangeTimerMode(object property)
        {
            if(_taskTimer.DispatcherTimer.IsEnabled)
            {
               if(!IsUserSureToContinue())
                {
                    return;
                }
            }

            switch(property as string)
            {
                case "Pomodoro":
                    TimerMode = TimerMode.Pomodoro;                   
                    break;
                case "ShortBreak":
                    TimerMode = TimerMode.ShortBreak;
                    break;
                case "LongBreak":
                    TimerMode = TimerMode.LongBreak;
                    break;
            }
        }

        public void UpdateTimerDuration()
        {
            _taskTimer.DispatcherTimer.Stop();          

            switch(TimerMode)
            {
                case TimerMode.Pomodoro:
                    _taskTimer.Duration = int.Parse(_databaseManager.ReadDataFromTheDatabase().ToList()[0]);
                    UpdateTaskTimeLeft(this, EventArgs.Empty);

                    break;
                case TimerMode.ShortBreak:
                    _taskTimer.Duration = int.Parse(_databaseManager.ReadDataFromTheDatabase().ToList()[1]);
                    UpdateTaskTimeLeft(this, EventArgs.Empty);
                    break;
                case TimerMode.LongBreak:
                    _taskTimer.Duration = int.Parse(_databaseManager.ReadDataFromTheDatabase().ToList()[2]);
                    UpdateTaskTimeLeft(this, EventArgs.Empty);
                    break;
            }
        }

        private bool IsUserSureToContinue()
        {
            CustomMessageBoxViewModel temporaryCustomMessageBoxViewModel = new CustomMessageBoxViewModel();
            UserChoice userAnswer = CustomMessageBox.CustomMessageBox.Show(temporaryCustomMessageBoxViewModel, "Warning", "We detected that the timer is currently runnig! Would you like to continue?");
            switch(userAnswer)
            {
                case UserChoice.OK:
                    return true;
                    break;
                case UserChoice.Cancel:
                    return false;
                    break;
                default:
                    return false;
                    break;
            }
        }
      
    }
}
