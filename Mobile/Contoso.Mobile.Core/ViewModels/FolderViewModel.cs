using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    [QueryProperty(nameof(FolderModel.Id), nameof(FolderModel.Id))]
    public sealed class FolderViewModel : BaseViewModel
    {
        #region Properties

        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { this.SetProperty(ref _Id, value); }
        }

        private FolderModel _Model;
        public FolderModel Model
        {
            get { return _Model; }
            private set { this.SetProperty(ref _Model, value); }
        }

        public Command NavigationCommand => new Command(async () => await this.NavigationAsync());

        private BaseItemModel _SelectedItem;
        public BaseItemModel SelectedItem
        {
            get { return _SelectedItem; }
            set { this.SetProperty(ref _SelectedItem, value); }
        }

        #endregion

        #region Constructors

        public FolderViewModel()
        {
            this.Title = "Folder";

            this.RefreshTasks.Add(this.LoadMainItems);
        }

        #endregion

        #region Methods

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if (this.Model == null || this.Model.Id != this.Id || forceRefresh)
                this.Model = (FolderModel)await this.DataStore.Notes.GetAsync(this.Id);

            this.Title = this.Model?.Name ?? "Welcome Folder";
        }

        private async Task LoadItems()
        { }

        private async Task NavigationAsync()
        {
            if (this.SelectedItem is FolderModel folder)
                await this.NavigationService.NavigateToAsync<FolderViewModel>(folder);
            else if (this.SelectedItem is NoteModel note)
                await this.NavigationService.NavigateToAsync<NoteViewModel>(note);

            this.SelectedItem = null;
        }

        #endregion
    }
}