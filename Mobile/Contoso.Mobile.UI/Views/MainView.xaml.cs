using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
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
            this.ViewModel = vm;
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is CollectionView control)
            {
                if (control.SelectedItem is FolderModel folder)
                {
                    await this.ViewModel?.NavigationService.NavigateToAsync<FolderViewModel>($"{nameof(BaseItemModel.Id)}={folder.Id}");
                }
                else if(control.SelectedItem is NoteModel note)
                {
                    await this.ViewModel?.NavigationService.NavigateToAsync<NoteViewModel>($"{nameof(BaseItemModel.Id)}={note.Id}");
                }
            }
        }
    }
}