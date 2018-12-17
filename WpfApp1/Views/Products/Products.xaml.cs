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
        public string Name = "Products";

        public Products()
        {
            InitializeComponent();
            this.DataContext = new ProductViewModel();
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
            this.Delete_Click(sender, e);
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
            var clientNetwork = new NetworkMiddleware.Client();
            
            if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Product>>(response.Reponse))
                    (this.DataContext as ViewModel.ProductViewModel).Products
                                        .Add(new Models.Product
                                        {
                                            Id = element.Id,
                                            Name = element.Name,
                                            Price = element.Price
                                        });
                
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Неудалось загрузить данные из бд! Пожалуйста, обратитесь к разработчику!");
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
            else
            {
                this.ShowMessageAsync("Ошибка!", "Невозможон обновить данные! Пожалуйста обратитесь к разработчику!");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.ProductViewModel).SelectedProduct != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_DELETE_CODE, (this.DataContext as ViewModel.ProductViewModel).SelectedProduct.Id))
                {
                    this.ShowMessageAsync("Операция выполнена успешно!", "Выбранный элемент был удалён из бд!");
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Невозможно удалить выбранный элемент из бд, т.к. он связан с другими записями в бд!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка","Для применения операция удалить необходимо выбрать элемент из таблицы!");
            }
        }
    }
}
