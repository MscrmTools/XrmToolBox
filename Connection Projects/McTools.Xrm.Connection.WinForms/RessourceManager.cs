using System.Drawing;
using System.IO;

namespace McTools.Xrm.Connection.WinForms
{
    public class RessourceManager
    {
        public static Image GetImage(string name)
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetAssembly(typeof(RessourceManager));

            using (Stream myStream = myAssembly.GetManifestResourceStream(name))
            {
                return new Bitmap(myStream);
            }
        }
    }
}