namespace MsCrmTools.AccessChecker
{
    internal class EntityInfo
    {
        public EntityInfo(string logicalName, string displayName, string primaryAttribute)
        {
            LogicalName = logicalName;
            DisplayName = displayName;
            PrimaryAttribute = primaryAttribute;
        }

        public string DisplayName { get; private set; }
        public string LogicalName { get; private set; }
        public string PrimaryAttribute { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", DisplayName, LogicalName);
        }
    }
}