using System;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    internal class CellInfo
    {
        public CellInfo(string cellId, string attributeLogicalName, Guid formId, string formName, string tabName, string sectionName)
        {
            CellId = new Guid(cellId);
            AttributeLogicalName = attributeLogicalName;
            FormId = formId;
            FormName = formName;
            TabName = tabName;
            SectionName = sectionName;
        }

        public String AttributeLogicalName { get; private set; }
        public Guid CellId { get; private set; }
        public Guid FormId { get; private set; }

        public string FormName { get; private set; }

        public string SectionName { get; private set; }
        public string TabName { get; private set; }
    }
}