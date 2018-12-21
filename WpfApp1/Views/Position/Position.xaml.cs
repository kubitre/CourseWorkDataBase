using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Position
{
    /// <summary>
    /// Interaction logic for Position.xaml
    /// </summary>
    public partial class Position : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public string NameClass = "Position";

        public Position()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.PositionViewModel();
            
            
        }

        private void CheckRigths()
        {
            if (this._memory.GetTypeApplication().Equals("client"))
            {
                this.AddNewElement.Visibility = System.Windows.Visibility.Hidden;
                this.RemoveElement.Visibility = System.Windows.Visibility.Hidden;
                this.PositionData.ContextMenu.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
            CheckRigths();
        }
        

        private void SettingPanelButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.SettingsPanel.IsOpen = true;
        private void AboutApplicationButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutApp.IsOpen = true;
        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e) => this.AboutMe.IsOpen = true;

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
            var windowAdding = new PositionAdd();

            windowAdding.SetMemoryDump(this._memory);
            windowAdding.ShowDialog();

            if ((bool)windowAdding.DialogResult)
            {
                this.ShowMessageAsync("Операция выполнена успешно!", "Новая должность была успешно добавлена в бд!");
                Refresh_Click(sender, e);
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Невозможон добавить новую дожность!");
            }
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PositionData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.PositionViewModel).SelectedPosition != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_DELETE_CODE, (this.DataContext as ViewModel.PositionViewModel).SelectedPosition.Id))
                {
                    Refresh_Click(sender, e);
                    this.ShowMessageAsync("Операция выполнена успешно!", "Выбранный элемент был успешно удалён из бд!");
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Выбранный элемент не может быть удалён из бд, т.к. он связан с записями из других таблиц!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "для выполнения операции удаления необходимо выбрать элемент!");
            }
        }

        private void PositionData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_GET_CODE, 100))
            {
                (this.DataContext as ViewModel.PositionViewModel).Positions.Clear();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Position>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.PositionViewModel).Positions.Add(new Models.Position
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
                }

            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Не удалось загрузить данные из бд! Пожалуйста, обратитесь к разработчику!");
            }
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
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
