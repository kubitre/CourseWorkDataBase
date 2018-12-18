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
    /// Interaction logic for CooperatorUpdate.xaml
    /// </summary>
    public partial class CooperatorUpdate : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private AdminPanel.Models.Cooperator selectedItem;

        public CooperatorUpdate()
        {
            InitializeComponent();
            this.DataContext = new NetworkMiddleware.NetworkData.Cooperator();

            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Street>>(response.Reponse))
                {
                    this.Addresses.Items.Add(element.Name);
                }
            }

            var clientNetworkPosition = new NetworkMiddleware.Client();
            if (clientNetworkPosition.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkPosition.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Position>>(response.Reponse))
                {
                    this.PositonCombo.Items.Add(element.Name);
                }
            }

            var clientNetworkCategory = new NetworkMiddleware.Client();
            if (clientNetworkCategory.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkCategory.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Category>>(response.Reponse))
                {
                    this.CategoryCombo.Items.Add(element.Name);
                }
            }
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        public void SetItem(NetworkMiddleware.NetworkData.Cooperator item)
        {
            this.DataContext = item;
            this.BirthDay.SelectedDate = item.BirthDay;
        }
        private void PositonCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Addresses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void UpdateCooperator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
