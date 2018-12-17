using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;

namespace AdminPanel.Views.User
{
    public partial class Users : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clientNetwork;

        public Users()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel();
            this._clientNetwork = new NetworkMiddleware.Client();
        }
        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void AboutApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void SettingPanelButton_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void ChangeElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TraceRoute_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            mainWindow.SetMemoryDump(this._memory);
            mainWindow.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, RoutedEventArgs e)
        {
            var windowForAdding = new UserAdd();
            this._memory.AddToHistory("UserAdd");
            windowForAdding.ShowDialog();
        }

        private void UsersData_Loaded(object sender, RoutedEventArgs e)
        {
            if(this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.UserCodes.USER_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clientNetwork.Response);

                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.UserNetwork>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.UserViewModel).Users.Add(new Models.User
                    {
                        Id = element.Id,
                        LastEnter = element.LastEnter,
                        Name = element.Username,
                        Role = element.Role,
                        Cooperator = element.Cooperator
                    });
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
