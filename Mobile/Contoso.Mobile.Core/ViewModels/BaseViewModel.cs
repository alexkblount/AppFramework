using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel : BaseModel
    {
        #region Properties

        private bool _IsInitialized;
        public bool IsInitialized
        {
            get { return _IsInitialized; }
            private set { this.SetProperty(ref _IsInitialized, value); }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            protected set { this.SetProperty(ref _Title, value); }
        }

        private bool _StatusIsBusy;
        public bool StatusIsBusy
        {
            get { return _StatusIsBusy; }
            set { this.SetProperty(ref _StatusIsBusy, value); }
        }

        private string _StatusText;
        public string StatusText
        {
            get { return _StatusText; }
            private set { this.SetProperty(ref _StatusText, value); }
        }

        private Command _RefreshCommand;
        public Command RefreshCommand
        {
            get { return _RefreshCommand; }
            private set { this.SetProperty(ref _RefreshCommand, value); }
        }

        #endregion

        #region Constructors

        public BaseViewModel()
        {
            this.RefreshCommand = new Command(async () => await RefreshAsync(true));
        }

        #endregion

        #region Methods

        protected void ShowBusyStatus(string statusMessage)
        {
            this.StatusIsBusy = true;

            if(!string.IsNullOrWhiteSpace(statusMessage))
                this.StatusText = statusMessage;
        }

        protected void ClearStatus()
        {
            this.StatusIsBusy = false;
            this.StatusText = null;
        }

        public async Task RefreshAsync(bool forceRefresh = false, string statusText = null)
        {
            try
            {
                this.ShowBusyStatus(statusText);
                await this.OnRefreshAsync(forceRefresh || !this.IsInitialized);
            }
            finally
            {
                this.IsInitialized = true;
                this.ClearStatus();
            }
        }

        protected virtual Task OnRefreshAsync(bool forceRefresh)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}