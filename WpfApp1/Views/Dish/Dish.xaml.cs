using AdminPanel.NetworkMiddleware;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AdminPanel.Views.Dish
{
    public partial class Dish : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public string Name = "Dish";

        public static event Events.EventForDataForm.AddNewData<NetworkMiddleware.NetworkData.Dish> Dishes;
        public Dish()
        {
            InitializeComponent();
            DataContext = new ViewModel.DishViewModel();

            Dishes += UpdateDataOnForm;
            (this.DishData as DataGrid).RowHeight = 100;
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void DishData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var networkClient = new NetworkMiddleware.Client();
            (this.DataContext as ViewModel.DishViewModel).Dishes.Clear();

            if(networkClient.RequestHandle(NetworkMiddleware.NetworkResponseCodes.DishCodes.DISH_GET_CODE, 100))
                foreach (var elementRecieved in (Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Dish>>(networkClient.State.LastResponse)))
                    (this.DataContext as ViewModel.DishViewModel).Dishes.Add(new Models.Dish() { Id = elementRecieved.Id, Name= elementRecieved.Name, OutPrice = elementRecieved.Outer.ToString(), Recipe = string.Join("\n", elementRecieved.Products.ToArray())});
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ShowMessageAsync("Изменение элемента", $"ID: {(this.DataContext as ViewModel.DishViewModel).SelectedDish.Id}; Name: {(this.DataContext as ViewModel.DishViewModel).SelectedDish.Name}");
        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clinetNetwork = new NetworkMiddleware.Client();
            if(clinetNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.DishCodes.DISH_DELETE_CODE, (this.DataContext as ViewModel.DishViewModel).SelectedDish.Id))
            {
                this.DishData_Loaded(sender, e);
                this.ShowMessageAsync("Операция выполнена успешно!", "Выбранное блюдо было удалено из бд!");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Выбранное блюдо невозможно удалить из бд, т.к. есть связи с другими таблицами!");
            }
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowAddingNewElement = new DishAdd();
            this._memory.AddToHistory("DishAdd");
            windowAddingNewElement.SetMemoryDump(this._memory);
            windowAddingNewElement.ShowDialog();

            if((bool)windowAddingNewElement.DialogResult)
            {
                this.ShowMessageAsync("Операция выполнена успешно!", "Новое блюдо было добавлено в бд!");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Не удалось добавить блюдо в бд!");
            }
        }

        private void TraceRoute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        public static void UpdateDataOnForm(List<NetworkMiddleware.NetworkData.Dish> dishes)
        {

        }

        private void SettingsPanels_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void AboutApplication_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DishData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveElement_Click(sender, e);
        }
    }
}
