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

        private IList<BaseItemModel> _Items;
        public IList<BaseItemModel> Items
        {
            get { return _Items; }
            private set { this.SetProperty(ref _Items, value); }
        }

        public MainViewModel()
        {
            this.Title = "My Notes";
        }

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if(this.Items == null || forceRefresh)
                this.Items = await this.DataStore.GetAsync();
        }
    }
}