using Microsoft.Xrm.Sdk;
using System;
using System.Windows.Forms;

namespace xrmtb.XrmToolBox.Controls
{
    public class CRMRecordEventArgs : EventArgs
    {
        private int columnIndex;
        private int rowIndex;
        private Entity entity;
        private string attribute;

        public CRMRecordEventArgs(int columnIndex, int rowIndex, Entity entity, string attribute)
        {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
            this.entity = entity;
            this.attribute = attribute;
        }

        public int ColumnIndex { get { return columnIndex; } }

        public int RowIndex { get { return rowIndex; } }

        public Entity Entity { get { return entity; } }

        public string Attribute { get { return attribute; } }

        public object Value { get { return entity != null && entity.Contains(attribute) ? entity[attribute] : null; } }
    }
}
