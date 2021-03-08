using Contoso.Mobile.Core.Models;
using Xamarin.Forms;

namespace Contoso.Mobile.UI.Controls
{
    public sealed class NoteFolderDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderDataTemplate { get; set; }
        public DataTemplate NoteDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is FolderModel folder)
                return this.FolderDataTemplate;
            else if (item is NoteModel note)
                return this.NoteDataTemplate;
            else
                return this.NoteDataTemplate;
        }
    }
}