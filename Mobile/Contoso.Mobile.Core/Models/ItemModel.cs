using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Mobile.Core.Models
{
    public sealed class ItemModel : BaseModel
    {
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { this.SetProperty(ref _Id, value); }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { this.SetProperty(ref _Text, value); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { this.SetProperty(ref _Description, value); }
        }
    }
}