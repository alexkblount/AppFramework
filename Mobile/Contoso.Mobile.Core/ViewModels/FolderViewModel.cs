using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    [QueryProperty(nameof(FolderModel.Id), nameof(FolderModel.Id))]
    public sealed class FolderViewModel : BaseViewModel
    {
        private IDataStore<BaseItemModel> DataStore => DependencyService.Get<IDataStore<BaseItemModel>>();

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

        public FolderViewModel()
        {
            this.Title = "Folder";
        }

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if (this.Model == null || forceRefresh)
                this.Model = (FolderModel)await this.DataStore.GetAsync(this.Id);
        }
    }
}