using AdminPanel.NetworkMiddleware;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading;
using System.Windows;


namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : MetroWindow
    {
   
        public LoginPage()
        {
            Client.GetExceptionOutput += GetMessageException;
            InitializeComponent();
            //Client.NewMess += GetMessageFromServer;
            //this.Dispatcher.Invoke(new ThreadStart(() => Client.Run()));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    var errorPanel = new ErrorConnect();
                    errorPanel.Show();
                    this.Close();
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
                    if (Client.RequestHandle(NetworkMiddleware.NetworkSignal.Authentification_action.ActionType, this.LoginUsernameTextBox.Text, this.LoginPasswordTextBox.Password))
                    {
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(5000);
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
                throw ex;
            }
        }

        private async void GetMessage(string mess) => await this.ShowMessageAsync("Ошибка!", "Пожалуйста, проверьте вводимые данные, т.к. мы обнаружили, что " + mess);

        private async void GetMessageAuth(string mess) => await this.ShowMessageAsync("Ошибка входа!", mess);

        private async void GetMessageException(string mess) => await this.Invoke(() => this.ShowMessageAsync("Ошибка!", mess));

        

    }
}
