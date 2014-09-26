// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/
using System;

namespace XrmToolBox
{
    internal class ExternalMethodCallerInfo<T>
    {
        public Action<T> ExternalAction { get; set; }
        public T Parameter { get; set; }
    }

    internal class ExternalMethodCallerInfo
    {
        public Action ExternalAction { get; set; }

        public ExternalMethodCallerInfo(Action action)
        {
            ExternalAction = action;
        }
    }
}