using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel : BaseModel, IDisposable
    {
        #region Variables

        public static string GetViewName<T>()
        {
            var type = typeof(T);

            var name = type.Name;

            if (!name.Contains("ViewModel"))
                throw new NotSupportedException("Your view model is not named correctly");

            return name.Remove(name.IndexOf("Model"));
        }

        private CancellationTokenSource _cts;

        #endregion

        #region Properties

        private static INavigationService _NavigationService = DependencyService.Get<INavigationService>(); 
        public INavigationService NavigationService
        {
            get { return _NavigationService; }
        }

        private static IDataStore _DataStore = DependencyService.Get<IDataStore>();
        public IDataStore DataStore
        {
            get { return _DataStore; }
        }

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

        private IAsyncCommand _RefreshCommand;
        public IAsyncCommand RefreshCommand
        {
            get { return _RefreshCommand; }
            private set { this.SetProperty(ref _RefreshCommand, value); }
        }

        private readonly NotifyTaskCompletionList _RefreshTasks = new NotifyTaskCompletionList();
        public NotifyTaskCompletionList RefreshTasks
        {
            get { return _RefreshTasks; }
        }

        #endregion

        #region Constructors

        public BaseViewModel()
        {
            this.RefreshCommand = new AsyncCommand(async () => await RefreshAsync(!this.IsInitialized));
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            this.DisposeCancellationToken();
        }

        private void DisposeCancellationToken()
        {
            if (_cts?.IsCancellationRequested == false)
                _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        protected void ShowBusyStatus(string statusMessage = null)
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

        public Task RefreshAsync()
        {
            return this.RefreshAsync(!this.IsInitialized);
        }

        private async Task RefreshAsync(bool forceRefresh, string statusText = null)
        {
            try
            {
                this.ShowBusyStatus(statusText);
                _cts = new CancellationTokenSource();
                this.RefreshTasks?.Refresh(forceRefresh, _cts.Token);
                await this.OnRefreshAsync(forceRefresh, _cts.Token);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.IsInitialized = true;
                this.ClearStatus();
                this.DisposeCancellationToken();
            }
        }

        protected virtual Task OnRefreshAsync(bool forceRefresh, CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Events

        #endregion



        public Command NavigationCommand => new Command(() => { });
    }
}