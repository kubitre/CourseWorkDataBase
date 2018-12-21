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
        private ManualResetEventSlim manualResetEventSlim;
        public LoginPage()
        {
            InitializeComponent();
            manualResetEventSlim = new ManualResetEventSlim(false);
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
                //var threadForValidatingAndCheckData = new Thread(new ThreadStart(CheckData));
                //threadForValidatingAndCheckData.Start();
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
                //this.ProgressAuthentificate.Visibility = Visibility.Visible;
                //threadForValidatingAndCheckData.Join();

                //this.StatusBar.Visibility = Visibility.Hidden;



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
                this.StatusBar.Value = 10;
                if (ValidatorsAndCheckers.Validation.Validate(0, this.LoginUsernameTextBox.Text) & ValidatorsAndCheckers.Validation.Validate(1, this.LoginPasswordTextBox.Password))
                {
                    this.StatusBar.Value = 20;
                    var clientNetwork = new NetworkMiddleware.Client();
                    this.StatusBar.Value = 30;
                    if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkSignal.Authentification_action.ActionTypeGet, this.LoginUsernameTextBox.Text, this.LoginPasswordTextBox.Password))
                    {
                        this.StatusBar.Value = 50;
                        var responseParse = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetwork.Response);
                        this.StatusBar.Value = 60;

                        if(
                            (responseParse.Reponse.Contains("0") | responseParse.Reponse.Contains("1")) & this._memory.GetTypeApplication().Equals("client")
                            )
                        {
                            this.ShowMessageAsync("Ошибка!", "Вы пытаетесь зайти как администратор в клиентское приложение! ");
                            return false;
                        }
                        if(
                            (responseParse.Reponse.Contains("2") | responseParse.Reponse.Contains("3") | responseParse.Reponse.Contains("4")) & this._memory.GetTypeApplication().Equals("admin")
                            )
                        {
                            this.ShowMessageAsync("Ошибка!", "Вы пытаетесь зайти как клиент в админское приложение! ");
                            return false;
                        }


                        this._memory.AddUser(new NetworkMiddleware.NetworkData.User()
                        {
                            UserName = this.LoginUsernameTextBox.Text,
                            AuthCode = responseParse.Reponse,
                            LastEnter = DateTime.Now
                        });
                        this.StatusBar.Value = 90;

                        this.manualResetEventSlim.Set();
                        this.StatusBar.Value = 100;
                        return true;
                        //return true;
                    }
                    else
                    {
                        this.StatusBar.Value = 0;
                        Thread.Sleep(1000);
                        this.ProgressAuthentificate.Visibility = Visibility.Hidden;
                        throw new Exception("пользователь не прошёл аутентификацию!");

                        //Thread.Sleep(5000);
                        //return false;
                    }
                }
                else
                {
                    GetMessageAuth("Введёные вами данные неверные, либо такой пользователь не существует в системе! Обратитесь к администратору или программисту!");
                    //Thread.Sleep(5000);
                    this.StatusBar.Value = 0;
                    Thread.Sleep(1000);
                    this.ProgressAuthentificate.Visibility = Visibility.Hidden;
                    return false;
                    //return false;
                }
            }
            catch(Exception ex)
            {
                return false;
                //GetMessage(ex.Message);
            }
        }
        //private void AboutMeButton_Click(object sender, RoutedEventArgs e) => this.AboutMe.IsOpen = true;
        private async void GetMessage(string mess) => await this.ShowMessageAsync("Ошибка!", mess);
        private async void GetMessageAuth(string mess) => await this.ShowMessageAsync("Ошибка входа!", mess);
        private async void GetMessageException(string mess) => await this.Invoke(() => this.ShowMessageAsync("Ошибка!", mess));

        private void AboutApplication_Click(object sender, RoutedEventArgs e)
        {
            //this.AboutApp.IsOpen = true;
        }

        private void SettingsPanels_Click(object sender, RoutedEventArgs e)
        {
            //this.SettingsPanel.IsOpen = true;
        }

        private void SwithcTheme_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
