using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AdminPanel.Views.Streets
{
    /// <summary>
    /// Interaction logic for StreetUpdate.xaml
    /// </summary>
    public partial class StreetUpdate : MetroWindow
    {
        public StreetUpdate()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (this.ShowMessageAsync("Вы уверены?", "Если да, то нажмите ок").IsCompleted)
            {
                this.DialogResult = false;
            }
        }
    }
}
