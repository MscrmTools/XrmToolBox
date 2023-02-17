using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.AppCode
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Checks if the current file contains types that implement
        /// IXrmToolBoxPlugin interface
        /// </summary>
        /// <param name="fi">Current file info</param>
        /// <returns>Value that indicates if the current file contains types that implement
        /// IXrmToolBoxPlugin interface</returns>
        public static bool ImplementsXrmToolBoxPlugin(this FileInfo fi)
        {
            var assembly = Assembly.LoadFile(fi.FullName);

            foreach (var type in assembly.GetTypes())
            {
                if (type.GetInterfaces().Contains(typeof(IXrmToolBoxPlugin)))
                {
                    return true;
                }
            }

            return false;
        }

        public static Image ResizeImage(this Image image, int width, int height)
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