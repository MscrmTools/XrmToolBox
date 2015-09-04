// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MsCrmTools.Iconator.AppCode
{
    public static class ImageHelper
    {
        /// <summary>
        /// Convert a WebResource to Image displayable into listviews
        /// </summary>
        /// <param name="contentImageList">Base 64 code</param>
        /// <returns>Image</returns>
        public static Image ConvertWebResContent(string contentImageList)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(contentImageList);
                var ms = new MemoryStream(imageBytes);

                var im = Image.FromStream(ms, true, true);

                return im;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error on ConvertWebResContent method : {0}", ex.InnerException.Message));
            }
        }

        /// <summary>
        /// method for resizing an image
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="width">new width of the image </param>
        /// <param name="height">new height of the image</param>
        /// <returns></returns>
        public static Image Resize(Image img, int width, int height)
        {
            try
            {
                var bmp = new Bitmap(width, height);
                var graphic = Graphics.FromImage(bmp);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(img, 0, 0, width, height);
                graphic.Dispose();
                return bmp;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error on Resize method : {0}", ex.InnerException.Message));
            }
        }
    }
}