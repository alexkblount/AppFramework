using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
using Contoso.Mobile.Core.ViewModels;
using Contoso.Mobile.UI.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Services
{
    public sealed class NavigationService : INavigationService
    {
        public Task AccountSignInAsync(string id = null)
        {
            return Shell.Current.GoToAsync("//LoginView");
        }

        public Task AccountSignUpAsync(string id = null)
        {
            return Shell.Current.GoToAsync(BaseViewModel.GetViewName<AccountCreateViewModel>());
        }

        public async Task NavigateToDetailAsync<T>(BaseModel model = null) where T : BaseViewModel
        {
            var url = $"{BaseViewModel.GetViewName<T>()}";

            if (model is BaseItemModel item)
                url = url + $"?{nameof(BaseItemModel.Id)}={item.Id}";
           
            await Shell.Current.GoToAsync(url);
        }

        public Task NavigateToRootAsync()
        {
            return Shell.Current.GoToAsync("//BaseFolderView");
        }
    }
}