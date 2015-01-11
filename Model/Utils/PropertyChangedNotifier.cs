using System;
using System.ComponentModel;

namespace Model.Utils
{
    [Serializable()]
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    } 
}
