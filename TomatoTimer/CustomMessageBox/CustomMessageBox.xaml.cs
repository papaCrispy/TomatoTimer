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
using System.Windows.Shapes;

namespace TomatoTimer.CustomMessageBox
{
    /// <summary>
    /// Logika interakcji dla klasy CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        static CustomMessageBox cMessageBox;
        static UserChoice dialogChoice = UserChoice.Cancel;

        public CustomMessageBox() : this(null)
        {
            InitializeComponent();
        }

        public CustomMessageBox(CustomMessageBoxViewModel customMessageBoxViewModelToBind)
        {
            this.DataContext = customMessageBoxViewModelToBind;
            InitializeComponent();
        }

        public static UserChoice Show(CustomMessageBoxViewModel cMessageBoxViewModelToBind, string MessageBoxTitle, string MessageBoxContent)
        {
            cMessageBoxViewModelToBind.WindowTitle = MessageBoxTitle;
            cMessageBoxViewModelToBind.WindowMessage = MessageBoxContent;
            cMessageBoxViewModelToBind.UserMadeChoice += OnUserMadeChoice;
            cMessageBox = new CustomMessageBox(cMessageBoxViewModelToBind);
            cMessageBox.ShowDialog();
            return dialogChoice;
        }

        private static void OnUserMadeChoice(object sender, UserChoice choiceMadeByUser)
        {
            dialogChoice = choiceMadeByUser;
            cMessageBox.Close();
        }
    }
}
