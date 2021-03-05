using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        private IDataStore<BaseItemModel> DataStore => DependencyService.Get<IDataStore<BaseItemModel>>();

        private IList<BaseItemModel> _Notes;
        public IList<BaseItemModel> Notes
        {
            get { return _Notes; }
            private set { this.SetProperty(ref _Notes, value); }
        }

        public MainViewModel()
        {
            this.Title = "My Notes";
        }

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if(this.Notes == null || forceRefresh)
                this.Notes = await this.DataStore.GetAsync();
        }
    }
}