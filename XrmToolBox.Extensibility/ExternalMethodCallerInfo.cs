// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/

using System;

namespace XrmToolBox.Extensibility
{
    public class ExternalMethodCallerInfo<T>
    {
        public Action<T> ExternalAction { get; set; }
        public T Parameter { get; set; }
    }

    public class ExternalMethodCallerInfo
    {
        public ExternalMethodCallerInfo(Action action)
        {
            ExternalAction = action;
        }

        public Action ExternalAction { get; set; }
    }
}