using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AdminPanel.Views.Position
{
    /// <summary>
    /// Interaction logic for PositionAdd.xaml
    /// </summary>
    public partial class PositionAdd : MetroWindow
    {
        public PositionAdd()
        {
            InitializeComponent();
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
    }
}
