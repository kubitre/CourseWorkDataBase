using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace AdminPanel.Views.Cooperator
{
    /// <summary>
    /// Interaction logic for CooperatorAdd.xaml
    /// </summary>
    public partial class CooperatorAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public CooperatorAdd()
        {
            InitializeComponent();

        }
        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));

            var clientNetwork = new NetworkMiddleware.Client();
            if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.StreetCodes.STREET_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Street>>(response.Reponse))
                {
                    this.Addresses.Items.Add(element.Name);
                }
            }

            var clientNetworkPosition = new NetworkMiddleware.Client();
            if(clientNetworkPosition.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkPosition.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Position>>(response.Reponse))
                {
                    this.PositonCombo.Items.Add(element.Name);
                }
            }

            var clientNetworkCategory = new NetworkMiddleware.Client();
            if (clientNetworkCategory.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkCategory.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Category>>(response.Reponse))
                {
                    this.CategoryCombo.Items.Add(element.Name);
                }
            }
        }

        private void CreateNewCooperator_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (
                !ChekcInput(this.FirstName_Input.Text, this.MiddleName_Input.Text, this.LastName_Input.Text, this.Building_Input.Text, this.Flat_Input.Text, this.salary_input.Text)
                )
            {
                this.ShowMessageAsync("Ошибка!", "Вы не ввели обязательные поля для ввода!");
            }
            if(this.Addresses.SelectedItem == null)
                this.ShowMessageAsync("Ошибка!", "Вы не выбрали улицу!");

            if(this.PositonCombo.SelectedItem == null)
                this.ShowMessageAsync("Ошибка!", "Вы не выбрали должность!");


            var cooperator = new NetworkMiddleware.NetworkData.Cooperator
            {
                FirstName = this.FirstName_Input.Text,
                MiddleName = this.MiddleName_Input.Text,
                LastName = this.LastName_Input.Text,
                Street = this.Addresses.SelectedItem.ToString(),
                Salary = double.Parse(this.salary_input.Text),
                BirthDay = this.BirthDay.DisplayDate,
                Building = int.Parse(this.Building_Input.Text),
                Category = this.CategoryCombo.SelectedItem.ToString(),
                Flat = int.Parse(this.Flat_Input.Text),
                Position = this.PositonCombo.SelectedItem.ToString()
            };
            
        }

        private bool ChekcInput(params string[] param)
        {
            foreach(var element in param)
            {
                if (string.IsNullOrEmpty(element) | string.IsNullOrWhiteSpace(element))
                {
                    return false;
                }
            }
            return true;
        }

        private void ListWithAdress_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Addresses_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void PositonCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void CategoryCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
