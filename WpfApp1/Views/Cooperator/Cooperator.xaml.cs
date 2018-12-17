using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
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

namespace AdminPanel.Views.Cooperator
{
    
    /// <summary>
    /// Interaction logic for Cooperator.xaml
    /// </summary>
    public partial class Cooperator : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clientNetwork;
        public Cooperator()
        {
            InitializeComponent();
            this.DataContext = new CooperatorViewModel();
            this._clientNetwork = new NetworkMiddleware.Client();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void ChangeElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TraceRoute_Click(object sender, RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, RoutedEventArgs e)
        {
            var windowForAdding = new CooperatorAdd();
            this._memory.AddToHistory("CooperatorAdd");
            windowForAdding.SetMemoryDump(this._memory);
            windowForAdding.ShowDialog();
        }

        private void CooperatorData_Loaded_1(object sender, RoutedEventArgs e)
        {
            this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE, 100);
            if(this._clientNetwork.Response != null)
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Cooperator>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.CooperatorViewModel).Cooperators.Add(new AdminPanel.Models.Cooperator
                    {
                        Id = element.Id,
                        Address = $"{element.Street} {element.Building}, кв. {element.Flat}",
                        Position = $"{element.Position} {element.Category}",
                        Price = element.Salary,
                        FMEName = $"{element.FirstName} {element.MiddleName[0]}. {element.LastName[0]}."
                    });
                }
            }
        }

        private void SettingsPanels_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void AboutApplication_Click(object sender, RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
