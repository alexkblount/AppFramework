using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
using Contoso.Mobile.Core.ViewModels;
using Contoso.Mobile.UI.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Services
{
    public sealed class NavigationService : INavigationService
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

        public Task AccountSignInAsync(string id = null)
        {
            return this.Navigation.PushAsync(new AccountLoginView());
        }

        public Task AccountSignUpAsync(string id = null)
        {
            return this.Navigation.PushAsync(new AccountCreateView());
        }

        public async Task HomeAsync()
        {
            if (Application.Current.MainPage.GetType() != typeof(ShellView))
                Application.Current.MainPage = new ShellView();
            else
                await Shell.Current.GoToAsync("...");
        }

        public async Task NavigateToAsync<T>(BaseModel model = null) where T : BaseViewModel
        {
            var url = $"{typeof(T).Name}";

            if (model is FolderModel folder)
                url = url + $"?{nameof(BaseItemModel.Id)}={folder.Id}";
            else if (model is NoteModel note)
                url = url + $"?{nameof(BaseItemModel.Id)}={note.Id}";

            await Shell.Current.GoToAsync(url);
        }
    }
}