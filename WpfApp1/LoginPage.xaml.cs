using AdminPanel.NetworkMiddleware;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading;
using System.Windows;


namespace AdminPanel
{
    public partial class LoginPage : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private Client _networkClient;

        public LoginPage()
        {
            InitializeComponent();
            this._networkClient = new Client();
            this._networkClient.GetExceptionOutput += GetMessageException;
                                        
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    var mainWindow = new MainWindow();
                    this._memory.AddToHistory("mainWindow");
                    mainWindow.SetMemoryDump(this._memory);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    var errorPanel = new ErrorConnect();
                    this._memory.AddToHistory("ErrorAuth");
                    errorPanel.SetMemoryDump(this._memory);
                    errorPanel.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                GetMessage(ex.Message);
            }
            
        }

        private bool CheckData()
        {
            try
            {
                if (ValidatorsAndCheckers.Validation.Validate(0, this.LoginUsernameTextBox.Text) & ValidatorsAndCheckers.Validation.Validate(1, this.LoginPasswordTextBox.Password))
                {
                    if (this._networkClient.RequestHandle(NetworkMiddleware.NetworkSignal.Authentification_action.ActionTypeGet, this.LoginUsernameTextBox.Text, this.LoginPasswordTextBox.Password))
                    {
                        var responseParse = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(this._networkClient.Response);

                        this._memory.AddUser(new NetworkMiddleware.NetworkData.User()
                        {
                            UserName = this.LoginUsernameTextBox.Text,
                            AuthCode = responseParse.Reponse,
                            LastEnter = DateTime.Now
                        });

                        return true;
                    }
                    else
                    {
                        //Thread.Sleep(5000);
                        return false;
                    }
                }
                else
                {
                    GetMessageAuth("Введёные вами данные неверные, либо такой пользователь не существует в системе! Обратитесь к администратору или программисту!");
                    Thread.Sleep(5000);
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
                GetMessage(ex.Message);
            }
        }
        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private async void GetMessage(string mess) => await this.ShowMessageAsync("Ошибка!", mess);

        private async void GetMessageAuth(string mess) => await this.ShowMessageAsync("Ошибка входа!", mess);

        private async void GetMessageException(string mess) => await this.Invoke(() => this.ShowMessageAsync("Ошибка!", mess));

        private void AboutApplication_Click(object sender, RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void SettingsPanels_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }
    }
}
