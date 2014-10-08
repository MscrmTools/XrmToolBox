using System.Globalization;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    class Language
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
