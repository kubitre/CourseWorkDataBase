using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AdminPanel.Views.Streets
{
    public partial class StreetsAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clientNetwork;
        private bool isUpdate = false;

        public StreetsAdd()
        {
            InitializeComponent();
            this._clientNetwork = new NetworkMiddleware.Client();
        }

        public StreetsAdd(Models.Street street) 
        {
            InitializeComponent();

            this.StreetName.Text = street.Name;

            this.Title = "Обновить название улицы";
            this.NewStreet.Content = "Обновить";
            isUpdate = true;
        }

        private void NewStreet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isUpdate)
            {
                if (this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_UPDATE_CODE, this.StreetName.Text))
                    this.DialogResult = true;
                else
                    this.DialogResult = false;

                this._clientNetwork.CloseSocket();
            }
            else
            {
                if (this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_CREATE_CODE, this.StreetName.Text))
                    this.DialogResult = true;
                else
                    this.DialogResult = false;

                this._clientNetwork.CloseSocket();
            }
            
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
