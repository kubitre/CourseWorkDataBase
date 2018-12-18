using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;

namespace AdminPanel.Views.User
{
    public partial class UserAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private List<NetworkMiddleware.NetworkData.Cooperator> _cooperators;

        public UserAdd()
        {
            InitializeComponent();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));

            var clientNetwork1 = new NetworkMiddleware.Client();
            if(clientNetwork1.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork1.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Cooperator>>(response.Reponse))
                {
                    this.CooperatorChoose.Items.Add(element.FirstName);
                }
            }

            this.RoleChoose.Items.Add("Администратор");
            this.RoleChoose.Items.Add("Программист");
            this.RoleChoose.Items.Add("Калькулятор");
        }

        private void CooperatorChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void RoleChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            var clientNetwork = new NetworkMiddleware.Client();
            if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.UserCodes.USER_CREATE_CODE, new NetworkMiddleware.NetworkData.UserNetwork
            {
                Cooperator = this.CooperatorChoose.SelectedItem.ToString(),
                Username = this.Username_Input.Text,
                Role = this.RoleChoose.SelectedItem.ToString(),
                Password = this.Password_Input.Text
            }
            ))
            {
                this.DialogResult = true;
            }
            else
                this.DialogResult = false;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
