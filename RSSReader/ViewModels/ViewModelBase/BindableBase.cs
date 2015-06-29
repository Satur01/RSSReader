using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RSSReader.ViewModels.ViewModelBase
{
    public class BindableBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
