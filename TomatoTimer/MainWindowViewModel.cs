using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TomatoTimer.Commands;
using TomatoTimer.Databases;
using TomatoTimer.Settings;
using TomatoTimer.TaskBacklog;
using TomatoTimer.Timer;

namespace TomatoTimer
{
    public class MainWindowViewModel
    {

        private MainWindow _applicationMainWindow;
        public MainWindow ApplicationMainWindow
        {
            get { return _applicationMainWindow; }
            private set { _applicationMainWindow = value; }
        }

        public RelayCommand OpenSettingsView
        {
            get;
            private set;
        }
        public RelayCommand OpenTimingView
        {
            get;
            private set;
        }

        private NavigationService _mainWindowNavigationService;
        public NavigationService MainWindowNavigationService
        {
            get { return _mainWindowNavigationService; }
            private set { _mainWindowNavigationService = value; }
        }

        private TimerPageView _timerPageView;
        private TaskBacklogPageView _taskBacklogPageView;

        private SettingsPageViewModel _settingsPageViewModel;
        private TimerPageViewModel _timerPageViewModel;

        private Page _settingsPageView;


        public MainWindowViewModel(MainWindow windowToAttach)
        {
            DatabaseManager appSettingsDatabaseManager = new DatabaseManager();
            
            ApplicationMainWindow = windowToAttach;
            MainWindowNavigationService = ApplicationMainWindow.AppNavigator.NavigationService;

            _settingsPageView = new SettingsPageView();
            _timerPageView = new TimerPageView();

            _settingsPageViewModel = (SettingsPageViewModel)_settingsPageView.DataContext;
            _timerPageViewModel = (TimerPageViewModel)_timerPageView.DataContext;
            _settingsPageViewModel.SettingsChangedEvent += _timerPageViewModel.UpdateTimerDuration;

            OpenSettingsView = new RelayCommand(OnOpenSettingView);
            OpenTimingView = new RelayCommand(OnOpenTimingView);

            MainWindowNavigationService.Navigate(_timerPageView);
        }

        private void OnOpenSettingView(object parameter)
        {
            MainWindowNavigationService.Navigate(_settingsPageView);
        }

        private void OnOpenTimingView(object parameter)
        {
            MainWindowNavigationService.Navigate(_timerPageView);
        }
    }
}
