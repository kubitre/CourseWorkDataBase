using MahApps.Metro;
using MahApps.Metro.Controls;
using NetworkMiddleware.NetworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AdminPanel.Views.Dish
{
    public partial class DishAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private List<NetworkMiddleware.NetworkData.Product> _selectedProducts;

        public DishAdd()
        {
            InitializeComponent();

            this.DataContext = new AdminPanel.ViewModel.ProductViewModel();

            this._selectedProducts = new List<NetworkMiddleware.NetworkData.Product>();

            
        }

        public DishAdd(Models.Dish dish)
        {
            InitializeComponent();
            this.DataContext = new AdminPanel.ViewModel.ProductViewModel();

            this._selectedProducts = new List<NetworkMiddleware.NetworkData.Product>();

            this.Name_Input.Text = dish.Name;
            
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }


        private void RemoveElement_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var element = (this.DataContext as ViewModel.ProductViewModel).SelectedProduct;
            (this.DataContext as ViewModel.ProductViewModel).Products.Remove((this.DataContext as ViewModel.ProductViewModel)
                .Products.FirstOrDefault(x => x.Name.Equals(element.Name)
                ));
        }

        private bool ChekcInput(params string[] param)
        {
            foreach (var element in param)
            {
                if (string.IsNullOrEmpty(element) | string.IsNullOrWhiteSpace(element))
                {
                    return false;
                }
            }
            return true;
        }

        private void ProductList_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Product>>(response.Reponse))
                {
                    this.ChooseProducts.Items.Add(element.Name);
                    this._selectedProducts.Add(element);
                }

            }

            var clientNetwork2 = new NetworkMiddleware.Client();
            if (clientNetwork2.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE, 100, "повар"))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork2.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Cooperator>>(response.Reponse))
                    this.CookcerList.Items.Add(element.FirstName);

            }
        }


        private void AddNewDish_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var listWithProducts = new List<Recipe>();
            foreach(var product in (this.DataContext as ViewModel.ProductViewModel).Products)
            {
                listWithProducts.Add(new Recipe
                {
                    IdProduct = product.Id,
                    Amount = product.Amount
                });
            }
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.DishCodes.DISH_CREATE_CODE, new NetworkMiddleware.NetworkData.Dish
            {
                Name = this.Name_Input.Text,
                Products = listWithProducts,
                Date = DateTime.Now,
                Cooperator = this.CookcerList.SelectedItem.ToString()
            }))
            {
                this.DialogResult = true;
                
            }
            else
            {
                this.DialogResult = false;
            }

        }

        private void CookcerList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ChooseProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach(var product in this._selectedProducts)
                if(product.Name.Equals((sender as ComboBox).SelectedValue))
                    (this.DataContext as ViewModel.ProductViewModel).Products.Add(new Models.Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price
                    });

            (sender as ComboBox).SelectedItem = null;
        }
    }
}
