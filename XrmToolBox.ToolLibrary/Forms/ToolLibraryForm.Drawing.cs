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
        private Font boldFont;
        private Image githubImage;
        private Image mvpImage;
        private Image newImage;
        private Image noLogoImage;
        private Image StarImage;

        private void InitImages()
        {
            mvpImage = ResizeImage(Resource.mvp, 32, 32);
            githubImage = ResizeImage(Resource.github, 32, 32);
            newImage = ResizeImage(Resource.New, 32, 32);
            StarImage = ResizeImage(Resource.star, 32, 32);
            noLogoImage = ResizeImage(Resource.NoLogo100, 60, 60);

            boldFont = new Font(lvTools.Font.FontFamily, lvTools.Font.Size + 2, FontStyle.Bold);
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
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y + 5);
            }
            catch
            {
                e.Graphics.DrawImage(noLogoImage, e.Bounds.X, e.Bounds.Y + 5);
            }

            if (plugin.Compatibilty != CompatibleState.Compatible)
            {
                e.Graphics.DrawImage(Resource.StatusInvalid, e.Bounds.X + 36, e.Bounds.Y + 42);
            }
            else if (plugin.Action == PackageInstallAction.Update)
            {
                e.Graphics.DrawImage(Resource.StatusUpdateAvailable, e.Bounds.X + 36, e.Bounds.Y + 42);
            }
            else if (plugin.Action == PackageInstallAction.None)
            {
                e.Graphics.DrawImage(Resource.StatusOK, e.Bounds.X + 36, e.Bounds.Y + 42);
            }

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;

            Rectangle rec = new Rectangle(e.Bounds.X + 60, e.Bounds.Y + 6, e.Bounds.Width - 20, e.Bounds.Height - 4);

            //If a different tabstop than the default is needed, will have to p/invoke DrawTextEx from win32.

            var shift = 40;

            TextRenderer.DrawText(e.Graphics, plugin.Name, boldFont, rec, e.Item.Selected ? Color.White : e.Item.ForeColor, flags);
            shift += TextRenderer.MeasureText(plugin.Name, boldFont).Width;

            var author = $"by {plugin.Authors}";
            TextRenderer.DrawText(e.Graphics, author, e.Item.ListView.Font, new Rectangle(e.Bounds.X + 20 + shift, e.Bounds.Y + 10, e.Bounds.Width - 20, e.Bounds.Height - 4), e.Item.Selected ? Color.White : e.Item.ForeColor, flags);
            shift += TextRenderer.MeasureText(author, e.Item.ListView.Font).Width;

            TextRenderer.DrawText(e.Graphics, plugin.Description, e.Item.ListView.Font, new Rectangle(e.Bounds.X + 60, e.Bounds.Y + 40, e.Bounds.Width - 20, e.Bounds.Height - 4), e.Item.Selected ? Color.White : e.Item.ForeColor, flags);

            if (!plugin.IsFromCustomRepo && (plugin.IsOpenSource ?? false))
            {
                e.Graphics.DrawImage(githubImage, e.Bounds.X + 60 + shift, e.Bounds.Y + 5);
                shift += 40;
            }

            if (!plugin.IsFromCustomRepo && (plugin.IsMvp ?? false))
            {
                e.Graphics.DrawImage(mvpImage, e.Bounds.X + 60 + shift, e.Bounds.Y + 5);
                shift += 40;
            }

            if (!plugin.IsFromCustomRepo && plugin.TotalFeedbackRating > settings.MostRatedMinNumberOfVotes && plugin.AverageFeedbackRating > settings.MostRatedMinRatingAverage)
            {
                e.Graphics.DrawImage(StarImage, e.Bounds.X + 60 + shift, e.Bounds.Y + 5);
                shift += 40;
            }

            if (plugin.FirstReleaseDate.Value > DateTime.Now.AddDays(-10))
            {
                e.Graphics.DrawImage(newImage, e.Bounds.X + 60 + shift, e.Bounds.Y + 5);
                shift += 40;
            }
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