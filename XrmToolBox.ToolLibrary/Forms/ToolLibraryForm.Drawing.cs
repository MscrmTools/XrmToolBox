using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.ToolLibrary.AppCode;

namespace XrmToolBox.ToolLibrary.Forms
{
    public partial class ToolLibraryForm : DockContent
    {
        const int listItemPadding = 4; // The space between each element in the list item
        const int imageSize = 40; // The size of the main plugin image in the list item
        const int overlayIconSize = 18; // The size of the overlay icons placed on top of the main image
        const int iconSize = 16; // The size of the icons in the list item

        private Font boldFont;
        private Image githubImage;
        private Image mvpImage;
        private Image newImage;
        private Image noLogoImage;
        private Image StarImage;

        private void InitImages()
        {
            mvpImage = ResizeImage(Resource.mvp, iconSize, iconSize);
            githubImage = ResizeImage(Resource.github, iconSize, iconSize);
            newImage = ResizeImage(Resource.New, iconSize, iconSize);
            StarImage = ResizeImage(Resource.star, iconSize, iconSize);
            noLogoImage = ResizeImage(Resource.NoLogo100, imageSize, imageSize);

            boldFont = new Font(lvTools.Font.FontFamily, lvTools.Font.Size + 2, FontStyle.Bold);
            imageList1.ImageSize = new Size(imageSize + 2 * listItemPadding, imageSize + 2 * listItemPadding);
        }

        private void lvTools_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.DrawBackground();
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(e.Header.Tag);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
                CheckBoxRenderer.DrawCheckBox(e.Graphics,
                    new Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                    value ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                    System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void lvTools_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.DrawDefault = true;
                return;
            }

            var plugin = (XtbPlugin)e.Item.Tag;

            // Draw the standard header background.
            e.DrawBackground();

            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
            }

            Image img = noLogoImage;

            if (!string.IsNullOrEmpty(plugin.LogoUrl) && !imageCache.Cache.ContainsKey(plugin.LogoUrl))
            {
                imageCache.AddImage(plugin.LogoUrl);
            }

            try
            {
                var tmpImg = imageCache.Cache[plugin.LogoUrl];
                img = tmpImg;
            }
            catch
            {
                if (plugin.LogoUrl == null)
                    img = noLogoImage;
                else if (imageCache.Cache.ContainsKey(plugin.LogoUrl))
                    img = imageCache.Cache[plugin.LogoUrl];
            }

            try
            {
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y + listItemPadding, imageSize, imageSize);
            }
            catch
            {
                e.Graphics.DrawImage(noLogoImage, e.Bounds.X, e.Bounds.Y + listItemPadding, imageSize, imageSize);
            }

            if (plugin.Compatibilty != CompatibleState.Compatible)
            {
                e.Graphics.DrawImage(Resource.StatusInvalid, e.Bounds.X + listItemPadding + imageSize - overlayIconSize, e.Bounds.Y + listItemPadding + imageSize - overlayIconSize, overlayIconSize, overlayIconSize);
            }
            else if (plugin.Action == PackageInstallAction.Update)
            {
                e.Graphics.DrawImage(Resource.StatusUpdateAvailable, e.Bounds.X + listItemPadding + imageSize - overlayIconSize, e.Bounds.Y + listItemPadding + imageSize - overlayIconSize, overlayIconSize, overlayIconSize);
            }
            else if (plugin.Action == PackageInstallAction.None)
            {
                e.Graphics.DrawImage(Resource.StatusOK, e.Bounds.X + listItemPadding + imageSize - overlayIconSize, e.Bounds.Y + listItemPadding + imageSize - overlayIconSize, overlayIconSize, overlayIconSize);
            }

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding;

            Rectangle rec = new Rectangle(e.Bounds.X + listItemPadding + imageSize + listItemPadding, e.Bounds.Y + listItemPadding, e.Bounds.Width - listItemPadding - imageSize - listItemPadding, e.Bounds.Height - listItemPadding);

            //If a different tabstop than the default is needed, will have to p/invoke DrawTextEx from win32.

            var shift = 0;

            TextRenderer.DrawText(e.Graphics, plugin.Name, boldFont, rec, e.Item.Selected ? SystemColors.HighlightText : e.Item.ForeColor, flags);
            var titleSize = TextRenderer.MeasureText(e.Graphics, plugin.Name + " ", boldFont, rec.Size, flags);
            shift += titleSize.Width;

            var author = $"by {plugin.Authors}";
            var boldBaseLine = GetBaseLine(e.Graphics, boldFont);
            var mainBaseLine = GetBaseLine(e.Graphics, e.Item.ListView.Font);
            TextRenderer.DrawText(e.Graphics, author, e.Item.ListView.Font, new Rectangle(rec.X + shift, (int)Math.Ceiling(rec.Y + boldBaseLine - mainBaseLine), rec.Width - shift, (int)(rec.Height - boldBaseLine + mainBaseLine)), e.Item.Selected ? SystemColors.HighlightText : e.Item.ForeColor, flags);
            shift += TextRenderer.MeasureText(e.Graphics, author, e.Item.ListView.Font).Width;

            TextRenderer.DrawText(e.Graphics, plugin.Description, e.Item.ListView.Font, new Rectangle(e.Bounds.X + listItemPadding + imageSize + listItemPadding, e.Bounds.Y + titleSize.Height + 2 * listItemPadding, e.Bounds.Width - listItemPadding - imageSize - listItemPadding, e.Bounds.Height - titleSize.Height - listItemPadding), e.Item.Selected ? SystemColors.HighlightText : e.Item.ForeColor, flags);

            if (!plugin.IsFromCustomRepo && (plugin.IsOpenSource ?? false))
            {
                e.Graphics.DrawImage(githubImage, rec.X + shift + listItemPadding, rec.Y, iconSize, iconSize);
                shift += listItemPadding + iconSize;
            }

            if (!plugin.IsFromCustomRepo && (plugin.IsMvp ?? false))
            {
                e.Graphics.DrawImage(mvpImage, rec.X + shift + listItemPadding, rec.Y, iconSize, iconSize);
                shift += listItemPadding + iconSize;
            }

            if (!plugin.IsFromCustomRepo && plugin.TotalFeedbackRating > settings.MostRatedMinNumberOfVotes && plugin.AverageFeedbackRating > settings.MostRatedMinRatingAverage)
            {
                e.Graphics.DrawImage(StarImage, rec.X + shift + listItemPadding, rec.Y, iconSize, iconSize);
                shift += listItemPadding + iconSize;
            }

            if (plugin.FirstReleaseDate.Value > DateTime.Now.AddDays(-10))
            {
                e.Graphics.DrawImage(newImage, rec.X + shift + listItemPadding, rec.Y, iconSize, iconSize);
                shift += listItemPadding + iconSize;
            }
        }

        private float GetBaseLine(Graphics g, Font font)
        {
            var lineSpace = font.FontFamily.GetLineSpacing(font.Style);
            var ascent = font.FontFamily.GetCellAscent(font.Style);
            var height = font.GetHeight(g);
            return height * ascent / lineSpace;
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