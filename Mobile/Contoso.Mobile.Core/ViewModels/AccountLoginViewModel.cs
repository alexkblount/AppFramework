using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public sealed class AccountLoginViewModel : BaseViewModel
    {
        #region Properties

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (this.SetProperty(ref _email, value))
                    this.SubmitCommand.ChangeCanExecute();
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (this.SetProperty(ref _Password, value))
                    this.SubmitCommand.ChangeCanExecute();
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            private set { this.SetProperty(ref _ErrorMessage, value); }
        }

        public Command SubmitCommand { get; private set; }

        #endregion

        #region Constructors

        public AccountLoginViewModel()
        {
            this.Title = "Login";
            this.SubmitCommand = new Command(async () => await this.SubmitAsync(), this.CheckIsValid);
        }

        #endregion

        #region Methods

        private async Task SubmitAsync()
        {
            try
            {
                this.ShowBusyStatus("Login in...");
                this.ErrorMessage = null;
                if (await this.DataStore.AuthenticateAsync(this.Email, this.Password))
                {
                    this.Reset();
                    await Shell.Current.GoToAsync("//BaseFolderView");
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            finally
            {
                this.ClearStatus();
            }
        }

        private bool CheckIsValid()
        {
            return !string.IsNullOrWhiteSpace(this.Email) && !string.IsNullOrWhiteSpace(this.Password);
        }

        private void Reset()
        {
            this.Email = null;
            this.Password = null;
            this.ErrorMessage = null;
        }

        #endregion
    }
}