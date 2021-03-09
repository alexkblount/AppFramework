using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public sealed class WelcomeViewModel : BaseViewModel
    {
        #region Properties

        public Command LoginCommand => new Command(async () => await this.NavigationService.AccountSignInAsync());
        public Command CreateCommand => new Command(async () => await this.NavigationService.AccountSignUpAsync());

        #endregion

        #region Constructors

        public WelcomeViewModel()
        {
            this.Title = "Welcome!";
        }

        #endregion
    }
}