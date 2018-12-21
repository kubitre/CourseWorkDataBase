using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Streets
{
    public partial class Street : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public string Name = "Street";

        public Street()
        {
            InitializeComponent();
            this.DataContext = new StreetViewModel();

        }

        private void CheckRights()
        {
            if (this._memory.GetTypeApplication().Equals("client"))
            {
                this.AddNewElement.Visibility = System.Windows.Visibility.Hidden;
                this.RemoveElement.Visibility = System.Windows.Visibility.Hidden;
                this.Delete.Visibility = System.Windows.Visibility.Hidden;
                this.StreetData.ContextMenu.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
            CheckRights();
        }


        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowChange = new StreetsAdd((this.DataContext as ViewModel.StreetViewModel).SelectedStreet);
            this._memory.AddToHistory("Change Street");
            windowChange.SetMemoryDump(this._memory);
            windowChange.ShowDialog();
        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((this.DataContext as ViewModel.StreetViewModel).SelectedStreet != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_DELETE_CODE, (this.DataContext as ViewModel.StreetViewModel).SelectedStreet.Id))
                {
                    this.ShowMessageAsync("Операция выполнена успешно!", "Выбранный элемент был удалён из бд!");
                    this.Refresh_Click(sender, e);
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Выбранный элемент не может быть удалён из бд, т.к. он связан с другими записями в других таблицах!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполнения данной операции необходимо выбрать элемент в таблице!");
            }
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForAdding = new StreetsAdd();
            this._memory.AddToHistory("StreetAdd");
            windowForAdding.ShowDialog();

            if ((bool)windowForAdding.DialogResult)
            {
                this.ShowMessageAsync("Операция выполнена успешно!", "Был добавлен новый элемент в бд!");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Неудалось завершить операцию корректно! Пожалуйста, обратитесь к разработчику!");
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

        private void AboutMeButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.AboutMe.IsOpen = true;
        private void AboutApplicationButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.AboutApp.IsOpen = true;
        private void SettingPanelButton_Click_1(object sender, System.Windows.RoutedEventArgs e) => this.SettingsPanel.IsOpen = true;

        private void StreetData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_GET_CODE, 100))
            {
                (this.DataContext as ViewModel.StreetViewModel).Streets.Clear();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);

                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Street>>(response.Reponse))
                    (this.DataContext as ViewModel.StreetViewModel).Streets.Add(new Models.Street
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Неудалось загрузить данные с сервера! Пожалуйста обратитесь к разработчику!");
            }

        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StreetData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveElement_Click(sender, e);
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            TraceRoute_Click(sender, e);
        }

        private void SwithcTheme_Click(object sender, System.Windows.RoutedEventArgs e)
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
