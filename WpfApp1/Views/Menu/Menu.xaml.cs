using AdminPanel.ApplicationMemory;
using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace AdminPanel.Views.Menu
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private NetworkMiddleware.Client _clentNetwork;

        public Menu()
        {
            InitializeComponent();
            this.DataContext = new MenuViewModel();
            this._clentNetwork = new NetworkMiddleware.Client();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }


        private void TraceRoute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForAdding = new Menuadd();
            this._memory.AddToHistory("MenuAdd");
            windowForAdding.SetMemoryDump(this._memory);
            windowForAdding.ShowDialog();
            if (windowForAdding.DialogResult.Value)
            {

            }
        }

        
        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void MenuData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._clentNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.MenuCodes.MENU_GET_CODE, 100);
            if(this._clentNetwork.Response != null)
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._clentNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminPanel.NetworkMiddleware.NetworkData.Menu>>(payload.Reponse))
                {
                    (this.DataContext as ViewModel.MenuViewModel).Menus.Add(new Models.Menu
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Calculator = element.Coocker,
                        Date = element.Date,
                        Outer = element.Outer.ToString(),
                        Dish = string.Join("\n", element.Dishes.ToArray())
                    });
                }
            }
        }

        private void SettingsPanelButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.SettingsPanel.IsOpen = true;
        private void AboutAppButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutApp.IsOpen = true;
        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutMe.IsOpen = true;

        private void Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var excel = ExportToXls.GenerateExcel(ExportToXls.ToDataTable((this.DataContext as ViewModel.MenuViewModel).Menus), this.MenuData);
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Menu_backup"; // Default file name
            dlg.DefaultExt = ".xls"; // Default file extension
           // dlg.Filter = "Excel sheets(.xls)"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                excel.Workbooks[1].SaveAs(System.IO.Path.Combine(filename));
            }



            excel.Workbooks[1].Close();
            excel.Quit();
        }

        

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetworkTest = new NetworkMiddleware.Client();
            if (clientNetworkTest.Response != null)
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkTest.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminPanel.NetworkMiddleware.NetworkData.Menu>>(payload.Reponse))
                {
                    (this.DataContext as ViewModel.MenuViewModel).Menus.Add(new Models.Menu
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Calculator = element.Coocker,
                        Date = element.Date,
                        Outer = element.Outer.ToString(),
                        Dish = string.Join("\n", element.Dishes.ToArray())
                    });
                }
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if( (this.DataContext as ViewModel.MenuViewModel).SelectedMenu != null)
            {
                (this.DataContext as ViewModel.MenuViewModel).Menus.Remove((this.DataContext as ViewModel.MenuViewModel).SelectedMenu);
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполнения операции Удалить, необходимо выбрать элемент в таблице!");
            }
        }
    }
}
