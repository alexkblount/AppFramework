using Contoso.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Views
{
    public partial class MainView : BaseView
    {
        public MainView()
        {
            InitializeComponent();
            this.ViewModel = new MainViewModel();
        }
    }
}