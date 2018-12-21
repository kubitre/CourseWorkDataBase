using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AdminPanel.Views.Menu
{
    public partial class Menuadd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private List<NetworkMiddleware.NetworkData.DishGet> _dishes;

        public Menuadd()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.DishViewModel();
            this._dishes = new List<NetworkMiddleware.NetworkData.DishGet>();

            
        }

        public Menuadd(NetworkMiddleware.NetworkData.Menu menu) : base()
        {
            this.MenuName_Input.Text = menu.Name;
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as ViewModel.DishViewModel).Dishes.Remove((this.DataContext as ViewModel.DishViewModel).SelectedDish);
        }

        private void AddMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.MenuName_Input.Text) | string.IsNullOrWhiteSpace(this.MenuName_Input.Text))
                this.ShowMessageAsync("Ошибка!", "Вы не ввели название меню!");
            if(this._dishes.Count == 0)
                this.ShowMessageAsync("Ошибка!", "Необходимо добавить в меню хотя бы одно блюдо!");


            var listWithDishes = new List<string>();
            var lisd = new List<NetworkMiddleware.NetworkData.DishGet>();

            foreach(var element in (this.DataContext as ViewModel.DishViewModel).Dishes)
            {
                var dishs = _dishes.FirstOrDefault(x => x.Id.Equals(element.Id));
                lisd.Add(new NetworkMiddleware.NetworkData.DishGet
                {
                    Id = dishs.Id,
                    Name = dishs.Name,
                    Outer = dishs.Outer,
                    Products = dishs.Products
                });

                listWithDishes.Add(dishs.Name);
            }

            var menu = new NetworkMiddleware.NetworkData.Menu
            {
                Name = this.MenuName_Input.Text,
                Date = Date.DisplayDate,
                Dishes = listWithDishes,
                Coocker = this._memory.GetUserName()
            };

            var clientNetwork = new NetworkMiddleware.Client();

            if (!clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.MenuCodes.MENU_CREATE_CODE, menu))
            {
                this.DialogResult = false;
            }
            else
                this.DialogResult = true;
        }

        private void MenuList_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.DishCodes.DISH_GET_CODE, 1000))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.DishGet>>(response.Reponse))
                {
                    this._dishes.Add(element);
                    this.DishChoose.Items.Add(element.Name);
                }
            }
        }

        private void DishChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null) ;
            else
            {
                foreach (var element in this._dishes)
                {
                    if ((sender as ComboBox).SelectedItem.Equals(element.Name))
                    {
                        (this.DataContext as ViewModel.DishViewModel).Dishes.Add(new Models.Dish
                        {
                            Id = element.Id,
                            Name = element.Name,
                            OutPrice = element.Outer.ToString(),
                            Recipe = string.Join("\n", element.Products)
                        });
                    }
                }

                (sender as ComboBox).SelectedItem = null;
            }
        }
    }
}
