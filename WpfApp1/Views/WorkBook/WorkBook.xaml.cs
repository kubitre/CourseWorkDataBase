using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.WorkBook
{
    /// <summary>
    /// Interaction logic for WorkBook.xaml
    /// </summary>
    public partial class WorkBook : MetroWindow
    {
        public string Name = "WorkBook";
        private ApplicationMemory.MemoryBuild _memory;

        public WorkBook()
        {
            InitializeComponent();
            DataContext = new ViewModel.WorkBookViewModel();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            this.TraceRoute_Click(sender, e);
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

        private void TraceRoute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForMainWindow = new MainWindow();
            this._memory.AddToHistory("MainMenu");
            windowForMainWindow.SetMemoryDump(this._memory);
            windowForMainWindow.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void WorkBookData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();

            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.WorkBookCodes.WORKBOOK_GET_CODE))
            {
                (this.DataContext as ViewModel.WorkBookViewModel).WorkBooks.Clear();
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);

                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.WorkBook>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.WorkBookViewModel).WorkBooks.Add(new Models.WorkBook
                    {
                        Id = element.Id,
                        FirstName = element.FirstName,
                        OrderDate = element.OrderDate,
                        OrderNumber = element.OrderNumber,
                        Position = element.Position,
                        Reason = element.Reason
                    });
                }
            }
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowChange = new WorkBookChange((this.DataContext as ViewModel.WorkBookViewModel).SelectedWorkBook);
            windowChange.ShowDialog();
        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetworkForRemoving = new NetworkMiddleware.Client();
            if(clientNetworkForRemoving.RequestHandle(NetworkMiddleware.NetworkResponseCodes.WorkBookCodes.WORKBOOK_DELETE_CODE, (this.DataContext as ViewModel.WorkBookViewModel).SelectedWorkBook.Id))
            {
                this.ShowMessageAsync("Успешное выполнение операциии", "Трудовая книжка была удалена");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "невозможно удалить трудовую книжку!");
            }
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

        private void Works_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();

            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.WorkBookCodes.WORKBOOK_GET_CODE))
            {
                (this.DataContext as ViewModel.WorkBookViewModel).WorkBooks.Clear();
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);

                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.WorkBook>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.WorkBookViewModel).WorkBooks.Add(new Models.WorkBook
                    {
                        Id = element.Id,
                        FirstName = element.FirstName,
                        OrderDate = element.OrderDate,
                        OrderNumber = element.OrderNumber,
                        Position = element.Position,
                        Reason = element.Reason
                    });
                }
            }
        }
    }
}
