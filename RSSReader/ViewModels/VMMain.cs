using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RSSReader.Models;
using RSSReader.Models.CustomEventArgs;
using RSSReader.ViewModels.ViewModelBase;

namespace RSSReader.ViewModels
{
    public class VMMain : BindableBase
    {

        public VMMain()
        {
            mainModel = new RSSModel();
            mainModel.OnRssDownloadCompleted += OnRssDownloadCompleted;
            _getRssCommand = new Lazy<DelegateCommand>(() => new DelegateCommand(GetRssCommandExecute));
        }

        #region Models

        private RSSModel mainModel;

        #endregion

        #region Commands

        private Lazy<DelegateCommand> _getRssCommand;

        public ICommand GetRssCommand
        {
            get { return _getRssCommand.Value; }
        }

        #endregion

        #region Fields

        private ObservableCollection<rssChannelItem> _items;

        #endregion

        #region Properties

        public ObservableCollection<rssChannelItem> Items
        {
            get { return _items ?? (_items = new ObservableCollection<rssChannelItem>()); }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private async void GetRssCommandExecute()
        {
            await mainModel.RssDownload();
        }

        #endregion

        #region Event handler

        private void OnRssDownloadCompleted(object sender, OnRssDownloadEventArgs e)
        {
            Items = new ObservableCollection<rssChannelItem>(e.Result.channel.item);
        }

        #endregion

    }
}
