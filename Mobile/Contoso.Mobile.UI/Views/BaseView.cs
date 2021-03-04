using Contoso.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Views
{
    public class BaseView : ContentPage
    {
        private BaseViewModel _viewModel;
        public BaseViewModel ViewModel
        {
            get { return _viewModel; }
            protected set { this.BindingContext = _viewModel = value; }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // TODO Properly call Task from a async void method
            await this.ViewModel?.RefreshAsync();
        }
    }
}