using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RSSReader.Models.CustomEventArgs;

namespace RSSReader.Models
{
    public class RSSModel
    {

        public EventHandler<OnRssDownloadEventArgs> OnRssDownloadCompleted { get; set; }


        public async Task RssDownload()
        {
            try
            {
                string url = "https://saturninopimentel.com/rss";
                using (HttpClient httpClient = new HttpClient())
                {
                    var result = await httpClient.GetStreamAsync(url);
                    XmlSerializer serializer = new XmlSerializer(typeof(rss));
                    var rssResult = (rss)serializer.Deserialize(result);
                    if (OnRssDownloadCompleted != null)
                    {
                        OnRssDownloadCompleted(this, new OnRssDownloadEventArgs(rssResult));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
