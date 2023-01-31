using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace XrmToolBox.ToolLibrary.AppCode
{
    internal class ImageCache
    {
        private readonly ConcurrentDictionary<string, Image> _cache;
        private readonly string _cachePath;
        private readonly HttpClient _httpClient;
        private ConcurrentDictionary<string, string> _errors;

        public ImageCache(string cachePath, HttpClient httpClient)
        {
            _cachePath = Path.Combine(cachePath, "ToolLogoCache");
            _cache = new ConcurrentDictionary<string, Image>();
            _errors = new ConcurrentDictionary<string, string>();
            _httpClient = httpClient;
        }

        public ConcurrentDictionary<string, Image> Cache => _cache;

        public void AddImage(string url)
        {
            if (url == null) return;
            if (_cache.ContainsKey(url)) return;
            Image img = null;
            try
            {
                var imageBytes = _httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult();

                using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    img = ResizeImage(Image.FromStream(ms, true), 60, 60);
                }
            }
            catch (Exception error)
            {
                img = ResizeImage(Resource.NoLogo100, 60, 60);
                if (!_errors.ContainsKey(url))
                    _errors.TryAdd(url, error.InnerException?.Message ?? error.Message);
            }
            finally
            {
                if (url != null && !_cache.ContainsKey(url))
                {
                    _cache.TryAdd(url, img);
                }
            }
        }

        public void Clean()
        {
            if (Directory.Exists(_cachePath)) return;

            foreach (var file in Directory.GetFiles(_cachePath))
            {
                File.Delete(file);
            }
        }

        public void Load()
        {
            if (!Directory.Exists(_cachePath)) return;

            var imageCacheIndex = Path.Combine(_cachePath, "imageCache.json");
            if (!File.Exists(imageCacheIndex)) return;

            var ja = JArray.Parse(File.ReadAllText(imageCacheIndex));
            foreach (JObject entry in ja)
            {
                _cache.TryAdd(entry["url"].ToString(), Image.FromFile(Path.Combine(_cachePath, entry["filename"].ToString())));
            }
        }

        public void Save()
        {
            if (!Directory.Exists(_cachePath))
            {
                Directory.CreateDirectory(_cachePath);
            }

            var ja = new JArray();
            int index = 0;
            var imageConverter = new ImageConverter();
            foreach (var url in _cache.Keys)
            {
                index++;
                try
                {
                    byte[] xByte = (byte[])imageConverter.ConvertTo(_cache[url], typeof(byte[]));
                    File.WriteAllBytes(Path.Combine(_cachePath, index.ToString()), xByte);

                    var jo = new JObject(new JProperty("url", url), new JProperty("filename", index));

                    if (_errors.ContainsKey(url))
                    {
                        jo.Add("error", _errors[url]);
                    }

                    ja.Add(jo);
                }
                catch { }
            }

            var content = ja.ToString();

            File.WriteAllText(Path.Combine(_cachePath, "imageCache.json"), content);
        }

        internal void Refresh(List<XtbPlugin> plugins)
        {
            Clean();
            Cache.Clear();
            _errors = new ConcurrentDictionary<string, string>();
            Parallel.ForEach(plugins,
                 tool =>
                 {
                     AddImage(tool.LogoUrl);
                 });

            Save();
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap newImage = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.CompositingMode = CompositingMode.SourceCopy;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    gr.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return newImage;
        }
    }
}