using Contoso.Mobile.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    [QueryProperty(nameof(NoteModel.Id), nameof(NoteModel.Id))]
    public sealed class NoteViewModel : BaseViewModel
    {
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { this.SetProperty(ref _Id, value); }
        }

        private NoteModel _Model;
        public NoteModel Model
        {
            get { return _Model; }
            private set { this.SetProperty(ref _Model, value); }
        }

        public NoteViewModel()
        {
            this.Title = "Note";

            this.DoWork1Task = this.RefreshTasks.Add<string>(async (ct) => await this.DoWork1Async());

            this.DoWork2Task = new NotifyTaskCompletion<string>(async (ct) => await this.DoWork2Async(ct));
            this.RefreshTasks.Add(DoWork2Task);
        }

        protected override async Task OnRefreshAsync(bool forceRefresh, CancellationToken ct)
        {
            if (this.Model == null || this.Model.Id != this.Id || forceRefresh)
                this.Model = (NoteModel)await this.DataStore.Notes.GetAsync(this.Id);
        }

        private NotifyTaskCompletion<string> _DoWork1Task;
        public NotifyTaskCompletion<string> DoWork1Task
        {
            get { return _DoWork1Task; }
            private set { this.SetProperty(ref _DoWork1Task, value); }
        }

        private NotifyTaskCompletion<string> _DoWork2Task;
        public NotifyTaskCompletion<string> DoWork2Task
        {
            get { return _DoWork2Task; }
            private set { this.SetProperty(ref _DoWork2Task, value); }
        }

        private Task<string> DoWork1Async()
        {
            var tcs = new TaskCompletionSource<string>();

            var _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    var message = "Hello";
                    await Task.Delay(3000);
                    tcs.SetResult(message);
                }
                catch(Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }

        private Task<string> DoWork2Async(CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<string>();

            var _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    var message = "World";
                    await Task.Delay(5000);
                    tcs.SetResult(message);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }

    }
}