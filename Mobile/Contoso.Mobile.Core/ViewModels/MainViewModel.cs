using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        #region Properties

        private IList<BaseItemModel> _Notes;
        public IList<BaseItemModel> Notes
        {
            get { return _Notes; }
            private set { this.SetProperty(ref _Notes, value); }
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

        public MainViewModel()
        {
            this.Title = "My Notes";
        }

        #endregion

        #region Methods

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if(this.Notes == null || forceRefresh)
                this.Notes = await this.DataStore.Notes.GetAsync();
        }

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