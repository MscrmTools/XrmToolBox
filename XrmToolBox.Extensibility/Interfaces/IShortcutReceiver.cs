using System.Windows.Forms;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IShortcutReceiver
    {
        void ReceiveKeyDownShortcut(KeyEventArgs e);

        void ReceiveKeyPressShortcut(KeyPressEventArgs e);

        void ReceiveKeyUpShortcut(KeyEventArgs e);

        void ReceivePreviewKeyDownShortcut(PreviewKeyDownEventArgs e);
    }
}