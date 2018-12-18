using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Windows;

namespace AdminPanel.Views.User
{
    public partial class Users : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public string Name = "Users";

        public Users()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel();
        }
        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void AboutApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void SettingPanelButton_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void ChangeElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void TraceRoute_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            mainWindow.SetMemoryDump(this._memory);
            mainWindow.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, RoutedEventArgs e)
        {
            var windowForAdding = new UserAdd();
            this._memory.AddToHistory("UserAdd");
            windowForAdding.SetMemoryDump(this._memory);
            windowForAdding.ShowDialog();
        }

        private void UsersData_Loaded(object sender, RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.UserCodes.USER_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);

                (this.DataContext as ViewModel.UserViewModel).Users.Clear();

                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.UserNetwork>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.UserViewModel).Users.Add(new Models.User
                    {
                        Id = element.Id,
                        LastEnter = element.LastEnter,
                        Name = element.Username,
                        Role = element.Role,
                        Cooperator = element.Cooperator
                    });
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Неудалось загрузить данные с сервера! Пожалуйста, обратитесь к разработчику!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UsersData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.UserViewModel).SelectedUser != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.UserCodes.USER_DELETE_CODE, (this.DataContext as ViewModel.UserViewModel).SelectedUser.Id))
                {
                    this.ShowMessageAsync("Операция выполнена успешно!", "Выбранный пользователь был удалён из бд!");
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Выбранный пользователь не может быть удалён, поскольку он связан с записями в других таблицах!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполнения операции удалить необходимо выбрать элемент в таблице!");
            }
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            TraceRoute_Click(sender, e);
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}
