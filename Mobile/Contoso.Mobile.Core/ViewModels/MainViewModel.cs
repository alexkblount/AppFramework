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
        private IDataStore<ItemModel> DataStore => DependencyService.Get<IDataStore<ItemModel>>();

        private IList<ItemModel> _Items;
        public IList<ItemModel> Items
        {
            get { return _Items; }
            private set { this.SetProperty(ref _Items, value); }
        }

        protected override async Task OnRefreshAsync(bool forceRefresh)
        {
            if(this.Items == null || forceRefresh)
                this.Items = await this.DataStore.GetAsync();
        }
    }
}