using Windows.Web.Syndication;

namespace RSSReader.Models.CustomEventArgs
{
    public class OnRssDownloadEventArgs
    {

        public OnRssDownloadEventArgs(rss result)
        {
            Result = result;
        }

        public rss Result { get; set; }
    }
}
