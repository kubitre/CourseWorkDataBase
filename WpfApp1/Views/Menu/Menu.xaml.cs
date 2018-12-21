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
        public string Name = "Menu";

        public Menu()
        {
            InitializeComponent();
            this.DataContext = new MenuViewModel();
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
                this.MenuData_Loaded(sender, e);
            }
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.MenuViewModel).SelectedMenu != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.MenuCodes.MENU_DELETE_CODE, (this.DataContext as ViewModel.MenuViewModel).SelectedMenu.Id))
                {
                    this.ShowMessageAsync("Операция выполнена успешно!", "Выбранное меню было успешно удалено из бд!");
                    this.MenuData_Loaded(sender, e);
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Невозможно удалить данное меню, т.к. оно связано с другими таблицами в бд!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполенния операции удаления элемента из списка необходимо выбрать этот элемент!");
            }
            
        }
        private void MenuData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clentNetwork = new NetworkMiddleware.Client();
            if(clentNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.MenuCodes.MENU_GET_CODE, 100))
            {
                (this.DataContext as ViewModel.MenuViewModel).Menus.Clear();

                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clentNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminPanel.NetworkMiddleware.NetworkData.Menu>>(payload.Reponse))
                {
                    (this.DataContext as ViewModel.MenuViewModel).Menus.Add(new Models.Menu
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Calculator = element.Coocker,
                        Date = element.Date.Date,
                        Outer = string.Join("\n", element.Outer.ToArray()),
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
            dlg.FileName = "Menu_backup"; 
            dlg.DefaultExt = ".xls"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                excel.Workbooks[1].SaveAs(System.IO.Path.Combine(filename));
            }
            excel.Workbooks[1].Close();
            excel.Quit();
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveElement_Click(sender, e);
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            Export_Click(sender, e);
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            
        }

        private void CommandBinding_Executed_1(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute_1(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            TraceRoute_Click(sender, e);
        }

        private void SwithcTheme_Click(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = (sender as ToggleSwitch);
            if ((bool)toggleSwitch.IsChecked)
            {
                this._memory.ChangeAppTheme("BaseLight");
            }
            else
            {
                this._memory.ChangeAppTheme("BaseDark");
            }


            ThemeManager.ChangeAppStyle(this,
                                                ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                                ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
    }
}
