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
        private Client _networkClient;

        public static event Events.EventForDataForm.AddNewData<NetworkMiddleware.NetworkData.Dish> Dishes;
        public Dish()
        {
            InitializeComponent();
            DataContext = new ViewModel.DishViewModel();
            this._networkClient = new Client();
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
            this._networkClient.RequestHandle(NetworkMiddleware.NetworkResponseCodes.DishCodes.DISH_GET_CODE, 100);
            if(this._networkClient.State.LastResponse != null)
                foreach (var elementRecieved in (Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Dish>>(this._networkClient.State.LastResponse)))
                    (this.DataContext as ViewModel.DishViewModel).Dishes.Add(new Models.Dish() { Id = elementRecieved.Id, Name= elementRecieved.Name, OutPrice = elementRecieved.Outer.ToString(), Recipe = string.Join("\n", elementRecieved.Products.ToArray())});
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ShowMessageAsync("Изменение элемента", $"ID: {(this.DataContext as ViewModel.DishViewModel).SelectedDish.Id}; Name: {(this.DataContext as ViewModel.DishViewModel).SelectedDish.Name}");
        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowAddingNewElement = new DishAdd();
            this._memory.AddToHistory("DishAdd");
            windowAddingNewElement.SetMemoryDump(this._memory);
            windowAddingNewElement.ShowDialog();
            
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

        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
