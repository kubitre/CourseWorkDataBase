using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Streets
{
    public partial class Street : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clientNetwork;

        public Street()
        {
            InitializeComponent();
            this.DataContext = new StreetViewModel();
            this._clientNetwork = new NetworkMiddleware.Client();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }


        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForAdding = new StreetsAdd();
            this._memory.AddToHistory("StreetAdd");
            windowForAdding.ShowDialog();
            if(this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_GET_CODE, 1000, (this.DataContext as ViewModel.StreetViewModel).Streets.Count))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Street>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.StreetViewModel).Streets.Add(new Models.Street
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
                }
            }

            this.ShowMessageAsync("Результат добавления", (bool)windowForAdding.DialogResult ? "Улица была успешно добавлена" : "К сожалению возникли неполадки при добавлении новой улицы! За более подробной информацией обратитесь к программисту!");
            
        }

        private void TraceRoute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        private void AboutMeButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.AboutMe.IsOpen = true;
        private void AboutApplicationButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.AboutApp.IsOpen = true;
        private void SettingPanelButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.SettingsPanel.IsOpen = true;
        
        private void StreetData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clientNetwork.Response);

                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Street>>(response.Reponse))
                    (this.DataContext as ViewModel.StreetViewModel).Streets.Add(new Models.Street
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
            }
            
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
