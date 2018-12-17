using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Products
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clientNetwork;
        public Products()
        {
            InitializeComponent();
            this.DataContext = new ProductViewModel();
            this._clientNetwork = new NetworkMiddleware.Client();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForAdding = new ProductsAdd();
            this._memory.AddToHistory("ProductAdd");
            windowForAdding.ShowDialog();
        }

        private void SettingPanelButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.SettingsPanel.IsOpen = true;
        private void AboutApplicationButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutApp.IsOpen = true;
        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutMe.IsOpen = true;
        

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TraceRoute_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        private void ProductsData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_GET_CODE, 100);
            if(this._clientNetwork.Response != null)
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Product>>(response.Reponse))
                    (this.DataContext as ViewModel.ProductViewModel).Products
                                        .Add(new Models.Product
                                        {
                                            Id = element.Id,
                                            Name = element.Name,
                                            Price = element.Price
                                        });
                
            }
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNet = new NetworkMiddleware.Client();
            clientNet.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_GET_CODE, 100);
            if (clientNet.Response != null)
            {
                (this.DataContext as ViewModel.ProductViewModel).Products.Clear();
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNet.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Product>>(response.Reponse))
                    (this.DataContext as ViewModel.ProductViewModel).Products
                                        .Add(new Models.Product
                                        {
                                            Id = element.Id,
                                            Name = element.Name,
                                            Price = element.Price
                                        });

            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.ProductViewModel).SelectedProduct != null)
            {
                (this.DataContext as ViewModel.ProductViewModel).Products.Remove((this.DataContext as ViewModel.ProductViewModel).SelectedProduct);
                this.ShowMessageAsync("Успешно","Элемент был успешно удалён из данной таблицы");
            }
            else
            {
                this.ShowMessageAsync("Ошибка","В процессе удаления элемента возникли ошибки!");
            }
        }
    }
}
