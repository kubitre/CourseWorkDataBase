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
        private bool IsUpdate = false;
        public CooperatorAdd()
        {
            InitializeComponent();

        }
        public CooperatorAdd(NetworkMiddleware.NetworkData.Cooperator coop)
        {
            InitializeComponent();

            this.FirstName_Input.Text = coop.FirstName;
            this.MiddleName_Input.Text = coop.MiddleName;
            this.LastName_Input.Text = coop.LastName;
            this.salary_input.Text = coop.Salary.ToString();
            this.Addresses.SelectedItem = coop.Street;
            this.Flat_Input.Text = coop.Flat.ToString();
            this.Building_Input.Text = coop.Building.ToString();
            this.CategoryCombo.SelectedItem = coop.Category;
            this.PositonCombo.SelectedItem = coop.Position;
            this.BirthDay.SelectedDate = coop.BirthDay;

            this.CreateNewCooperator.Content = "Обновить данные";
            this.Title = "Обновление информации о сотруднике";

            IsUpdate = true;
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

            var catogoryName = "";

            if (this.CategoryCombo.SelectedItem != null)
                catogoryName = this.CategoryCombo.SelectedItem.ToString();


            var cooperator = new NetworkMiddleware.NetworkData.Cooperator
            {
                FirstName = this.FirstName_Input.Text,
                MiddleName = this.MiddleName_Input.Text,
                LastName = this.LastName_Input.Text,
                Street = this.Addresses.SelectedItem.ToString(),
                Salary = double.Parse(this.salary_input.Text),
                BirthDay = this.BirthDay.DisplayDate,
                Building = int.Parse(this.Building_Input.Text),
                Category = catogoryName,
                Flat = int.Parse(this.Flat_Input.Text),
                Position = this.PositonCombo.SelectedItem.ToString()
            };

            var clientNetwork = new NetworkMiddleware.Client();

            if (IsUpdate)
            {
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_UPDATE_CODE, cooperator))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_CREATE_CODE, cooperator))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            
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
