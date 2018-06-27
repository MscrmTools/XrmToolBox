using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Guid = System.Guid;

namespace XrmToolBox.Announcement
{
    #region DTO

    [DataContract]
    public class AnnouncementItem
    {
        [DataMember(Name = "mctools_announcementid")]
        public string AnnouncementId { get; set; }

        public Guid Id => new Guid(AnnouncementId);

        public Image Image
        {
            get
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(ImageUrl);
                MemoryStream ms = new MemoryStream(bytes);
                return Image.FromStream(ms);
            }
        }

        [DataMember(Name = "mctools_imageurl")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "mctools_name")]
        public string Name { get; set; }

        [DataMember(Name = "mctools_url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class RootAnnouncementobject
    {
        [DataMember(Name = "value")]
        public AnnouncementItem[] Items { get; set; }

        [DataMember(Name = "odatametadata")]
        public string OdataMetadata { get; set; }
    }

    #endregion DTO

    internal class AnnouncementManager
    {
        public static void Display(AnnouncementItem item)
        {
            if (item == null) return;

            var dialog = new AnnouncementDialog(item);
            if (!dialog.IsInvalid)
                dialog.Show();
        }

        public static AnnouncementItem GetItemToDisplay()
        {
            try
            {
                if (DateTime.Now < AnnouncementSettings.Instance.LastShownDate.AddDays(1))
                    return null;

                var news = GetContent<RootAnnouncementobject>(
                    "https://xrmtoolboxdev.microsoftcrmportals.com/_odata/announcementSet");

                var item = news.Items.FirstOrDefault(v =>
                    !AnnouncementSettings.Instance.ToHide.Contains(v.Id)
                    && !AnnouncementSettings.Instance.LastShown.Contains(v.Id));

                if (item != null)
                {
                    return item;
                }

                return news.Items.FirstOrDefault(v => v.Id == AnnouncementSettings.Instance.LastShown.FirstOrDefault());
            }
            catch
            {
                return null;
            }
        }

        private static T GetContent<T>(string url)
        {
            var request = WebRequest.CreateHttp(url);
            var response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream != null)
                {
                    var serializer = new DataContractJsonSerializer(typeof(T),
                        new DataContractJsonSerializerSettings
                        {
                            UseSimpleDictionaryFormat = true,
                            DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss", new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                        });

                    return (T)serializer.ReadObject(dataStream);
                }
            }

            return default(T);
        }
    }
}