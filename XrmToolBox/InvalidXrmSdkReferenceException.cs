using System;

namespace XrmToolBox
{
    [Serializable()]
    public class InvalidXrmSdkReferenceException : Exception
    {
        public InvalidXrmSdkReferenceException(string format, params object[] args)
            : base(String.Format(format, args))
        {
        }
    }
}