using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contoso.Mobile.Core.Models
{
    public abstract class BaseItemModel : BaseModel
    {
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { this.SetProperty(ref _Id, value); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { this.SetProperty(ref _Name, value); }
        }
    }

    public sealed class FolderModel : BaseItemModel
    {
        private ObservableCollection<BaseItemModel> _Notes;
        public ObservableCollection<BaseItemModel> Notes
        {
            get { return _Notes; }
            set { this.SetProperty(ref _Notes, value); }
        }
    }

    public sealed class NoteModel : BaseItemModel
    {
        private string _Body;
        public string Body
        {
            get { return _Body; }
            set { this.SetProperty(ref _Body, value); }
        }
    }
}