using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.FormRelated.FormLibrariesManager.POCO
{
    [DebuggerDisplay("{Name}")]
    public class Script : IEquatable<Script>
    {
        public string Name { get; set; }

        #region IEquatable<Script> Members

        public bool Equals(Script other)
        {
            return other != null && other.Name == this.Name;
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as Script;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
