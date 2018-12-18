using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AdminPanel.Views.Position
{
    /// <summary>
    /// Interaction logic for PositionAdd.xaml
    /// </summary>
    public partial class PositionAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public PositionAdd()
        {
            InitializeComponent();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void AddNewPosition_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Position_Input.Text) | !string.IsNullOrWhiteSpace(this.Position_Input.Text))
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_CREATE_CODE, this.Position_Input.Text))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка ввода!", "Вы не заполнили поле!");
            }
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}
