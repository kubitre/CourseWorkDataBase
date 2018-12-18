using AdminPanel.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminPanel.Views.Cooperator
{
    
    /// <summary>
    /// Interaction logic for Cooperator.xaml
    /// </summary>
    public partial class Cooperator : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private List<NetworkMiddleware.NetworkData.Cooperator> cooperators;

        public string Name = "Cooperator";
        public Cooperator()
        {
            InitializeComponent();
            this.DataContext = new CooperatorViewModel();
            this.cooperators = new List<NetworkMiddleware.NetworkData.Cooperator>();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void ChangeElement_Click(object sender, RoutedEventArgs e)
        {
            var element = cooperators.FirstOrDefault(x => x.Id.Equals((this.DataContext as ViewModel.CooperatorViewModel).SelectedCooperator.Id));
            var windowForUpdate = new CooperatorUpdate();
            windowForUpdate.SetMemoryDump(this._memory);
            windowForUpdate.SetItem(element);
            windowForUpdate.ShowDialog();
        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void TraceRoute_Click(object sender, RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            this._memory.AddToHistory("MainWindow");
            windowMain.SetMemoryDump(this._memory);
            windowMain.Show();
            this.Close();
        }

        private void AddNewElement_Click(object sender, RoutedEventArgs e)
        {
            var windowForAdding = new CooperatorAdd();
            this._memory.AddToHistory("CooperatorAdd");
            windowForAdding.SetMemoryDump(this._memory);
            windowForAdding.ShowDialog();
            if ((bool)windowForAdding.DialogResult)
            {
                this.ShowMessageAsync("Операция выполнена успешно!", "Добавлен новый пользователь в бд!");
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Ошибка добавления нового пользователя в бд!");
            }
        }

        private void CooperatorData_Loaded_1(object sender, RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();

            if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE, 100))
            {
                (this.DataContext as ViewModel.CooperatorViewModel).Cooperators.Clear();
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Cooperator>>(response.Reponse))
                {
                    cooperators.Add(element);
                    var category = "";

                    if (element.Category != null)
                        category = element.Category;

                    (this.DataContext as ViewModel.CooperatorViewModel).Cooperators.Add(new AdminPanel.Models.Cooperator
                    {
                        Id = element.Id,
                        Address = $"{element.Street} {element.Building}, кв. {element.Flat}",
                        Position = $"{element.Position} {category}",
                        Price = element.Salary,
                        FMEName = $"{element.FirstName} {element.MiddleName[0]}. {element.LastName[0]}."
                    });
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка загрузки данных!", "Пожалуйста, свяжитесь с программистом для решения проблемы!");
            }
        }

        private void SettingsPanels_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void AboutApplication_Click(object sender, RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if((this.DataContext as ViewModel.CooperatorViewModel).SelectedCooperator != null)
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_DELETE_CODE, (this.DataContext as ViewModel.CooperatorViewModel).SelectedCooperator.Id))
                {
                    this.CooperatorData_Loaded_1(sender, e);
                    this.ShowMessageAsync("Опреация выполнена успешно!", "Выбранный сотрудник был удалён из базы данных!");
                }
                else
                {
                    this.ShowMessageAsync("Ошибка!", "Невозможно удалить сотрудника, т.к. он связан с другими записями в бд!");
                }
            }
            else
            {
                this.ShowMessageAsync("Ошибка!", "Для выполнения операции удаления необходимо выбрать элемент из таблицы!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CooperatorData_Loaded_1(sender, e);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
