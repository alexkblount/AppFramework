using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.Services
{
    public sealed class NavigationService
    {
        public INavigation Navigation 
        { 
            get
            {
                if (Application.Current.MainPage is TabbedPage tabPage)
                {
                    return tabPage.CurrentPage.Navigation;
                }
                else if (Application.Current.MainPage is FlyoutPage flyPage)
                {
                    if (flyPage.Detail is TabbedPage flyTabbedPage)
                        return flyTabbedPage.CurrentPage.Navigation;
                    else
                        return flyPage.Detail.Navigation;
                }
                else
                {
                    return Application.Current.MainPage.Navigation;
                }
            }
        }

        public async Task NavigateToAsync<T>(object parameter = null) where T : BaseViewModel
        {
            var url = $"{typeof(T).Name}";
            if (parameter != null)
                url = url + "?" + parameter.ToString();

            await Shell.Current.GoToAsync(url); ;
        }
    }
}