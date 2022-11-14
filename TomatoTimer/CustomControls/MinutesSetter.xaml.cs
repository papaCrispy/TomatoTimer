using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TomatoTimer.CustomControls
{
    /// <summary>
    /// Logika interakcji dla klasy MinutesSetter.xaml
    /// </summary>
    public partial class MinutesSetter : UserControl
    {
        public static DependencyProperty MinutesValueProperty = DependencyProperty.Register("MinutesValue", typeof(string), typeof(MinutesSetter));

        public string MinutesValue
        {
            get { return (string)GetValue(MinutesValueProperty); }
            set { SetValue(MinutesValueProperty, value);
                MinutesValueTextBox.Text = MinutesValue;  }
        }

        public MinutesSetter()
        {
            InitializeComponent();
        }

        private void MinutesValueTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            MinutesValueTextBox.Text = MinutesValue;
        }

        private void AddMinutes_Click(object sender, RoutedEventArgs e)
        {
            int temporaryValue = int.Parse(MinutesValue);
            temporaryValue += 1;
            MinutesValue = temporaryValue.ToString();
        }

        private void RemoveMinutes_Click(object sender, RoutedEventArgs e)
        {
            int temporaryValue = int.Parse(MinutesValue);
            temporaryValue -= 1;
            MinutesValue = temporaryValue.ToString();
        }
    }
}
