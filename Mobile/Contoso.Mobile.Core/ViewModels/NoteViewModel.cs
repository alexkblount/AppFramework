using Contoso.Mobile.Core.Models;
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
        }

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if (this.Model == null || this.Model.Id != this.Id || forceRefresh)
                this.Model = (NoteModel)await this.DataStore.Notes.GetAsync(this.Id);
        }
    }
}