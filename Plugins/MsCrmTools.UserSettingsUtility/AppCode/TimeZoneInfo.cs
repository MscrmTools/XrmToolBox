namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class TimeZone
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}