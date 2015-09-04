using System.Globalization;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class Language
    {
        public Language(int lcid)
        {
            Lcid = lcid;
        }

        public int Lcid { get; private set; }

        public override string ToString()
        {
            return new CultureInfo(Lcid).EnglishName;
        }
    }
}