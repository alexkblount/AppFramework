using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.Core.ViewModels
{
    public sealed class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            this.Title = "About";
        }
    }
}