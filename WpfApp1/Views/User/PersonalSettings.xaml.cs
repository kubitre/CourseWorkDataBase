using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;

namespace AdminPanel.Views.User
{
    public partial class PersonalSettings : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public PersonalSettings()
        {
            InitializeComponent();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
        private void ChangePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.OldPassword.Password) | string.IsNullOrWhiteSpace(this.OldPassword.Password))
            {
                this.ShowMessageAsync("Ошибка!", "Вы ничего не ввели в поле старого пароля!");
                return;
            }

            if (string.IsNullOrEmpty(this.newPassword.Password) | string.IsNullOrWhiteSpace(this.newPassword.Password))
            {
                this.ShowMessageAsync("Ошибка!", "Вы ничего не ввели в поле нового пароля!");
                return;
            }
            try
            {
                var resultCheck = ValidatorsAndCheckers.Validation.Validate(1, this.newPassword.Password);
                if (!resultCheck)
                {
                    this.ShowMessageAsync("Ошибка!", "Введённый пароль не прошёл валидацию! Пожалуйста, соблюдайте правило: не менее 8 символов, где есть строчные либо заглавные буквы и цифры!");
                    return;
                }
                else
                {
                    var messageForSend = new NetworkMiddleware.NetworkData.PasswordChange
                    {
                        Username = this._memory.GetUserName(),
                        OldPassword = this.OldPassword.Password,
                        NewPassword = this.newPassword.Password
                    };

                    var clientNetwork = new NetworkMiddleware.Client();

                    if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PasswordChangeCodes.ActionType, messageForSend))
                    {
                        this.DialogResult = true;
                    }
                    else
                        this.DialogResult = false;
                }
            }
            catch(Exception ex)
            {
                this.ShowMessageAsync("Ошибка!", ex.Message);
            }
            
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
