using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.Mobile.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Busy : ContentView
    {
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(Busy), default(bool), BindingMode.OneWay);

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(Busy), default(string), BindingMode.OneWay);

        public Busy()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
    }
}