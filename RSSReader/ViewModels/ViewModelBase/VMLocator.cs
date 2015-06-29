using System;
using System.Windows.Input;

namespace RSSReader.ViewModels.ViewModelBase
{
    public class VMLocator
    {
        public VMLocator()
        {
            _vMMain = new Lazy<VMMain>(() => new VMMain());
        }

        private Lazy<VMMain> _vMMain;

        public VMMain VMMain
        {
            get { return _vMMain.Value; }
        }
    }
}
