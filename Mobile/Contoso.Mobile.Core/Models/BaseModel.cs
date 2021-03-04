using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contoso.Mobile.Core.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null, Action onChanged = null)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            else
            {
                storage = value;
                onChanged?.Invoke();
                this.NotifyPropertyChanged(propertyName);
                return true;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}