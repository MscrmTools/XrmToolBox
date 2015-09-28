using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.FormRelated.FormLibrariesManager.POCO
{
    [DebuggerDisplay("{Script} - {Function}")]
    public class FormEvent : IEquatable<FormEvent>
    {
        public string Script { get; set; }
        public string Function { get; set; }
        public bool Enabled { get; set; }
        public bool PassExecutionContext { get; set; }
        public string Parameters { get; set; }

        #region IEquatable<Project> Members

        public bool Equals(FormEvent other)
        {
            return other != null && other.Script == Script && other.Function == Function;
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as FormEvent;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return (Script + Function).GetHashCode();
        }
    }
}
