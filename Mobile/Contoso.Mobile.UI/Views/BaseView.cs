using Contoso.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Views
{
    public class BaseView : ContentPage
    {
        public BaseViewModel ViewModel
        {
            get 
            {
                if (this.BindingContext is BaseViewModel vm)
                    return vm;
                else
                    return null;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // TODO Properly call Task from a async void method
            await this.ViewModel?.RefreshAsync();
        }
    }
}