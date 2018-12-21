using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Category
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public string Name = "Category";

        public Category()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.CategoryViewModel();
        }

        private void CheckRights()
        {
            if (this._memory.GetTypeApplication().Equals("client"))
            {
                this.AddNewElement.Visibility = System.Windows.Visibility.Hidden;
                this.RemoveElement.Visibility = System.Windows.Visibility.Hidden;
                this.CategoryData.ContextMenu.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(_memory.GetAppTheme()));
            CheckRights();
        }

        private void SettingPanelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void AboutApplicationButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowForAdding = new CategoryAdd();
            this._memory.AddToHistory("Category Add");

            windowForAdding.SetMemoryDump(this._memory);
            windowForAdding.ShowDialog();

            if ((bool)windowForAdding.DialogResult)
            {
                this.ShowMessageAsync("Операция выполнена успешно!", "Новая категория была добавлена в бд!");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "К сожалению не удалось добавить новую категорию!");
            }
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CategoryData_Loaded(sender, e);
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveElement_Click(sender, e);
        }

        private void CategoryData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_GET_CODE, 100))
            {
                (this.DataContext as ViewModel.CategoryViewModel).Categories.Clear();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Category>>(response.Reponse))
                {
                    (this.DataContext as ViewModel.CategoryViewModel).Categories.Add(new Models.Category
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "К соажелнию, не удалось получит данные с сервера! Пожалуйста, обратитесь к разработчику!");
            }
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.CategoryViewModel).SelectedCategory != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_DELETE_CODE, (this.DataContext as ViewModel.CategoryViewModel).SelectedCategory.Id))
                {
                    this.ShowMessageAsync("Операция была выполнена успешно!", "Выбранный элемент успешно удалён из бд!");
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Выбранный элемент не может быть удалён, т.к. на него\\ он ссылаются\\ется другие записи в бд");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполнения операции удаления необходимо выбрать элемент из таблицы!");
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
