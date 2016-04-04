// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ServiceModel;

namespace MsCrmTools.ViewLayoutReplicator
{
    public class CrmExceptionHelper
    {
        public static string GetErrorMessage(Exception error, bool returnWithStackTrace)
        {
            if (error.InnerException is FaultException)
            {
                if (returnWithStackTrace)
                {
                    return (error.InnerException).ToString();
                }
                else
                {
                    return (error.InnerException).Message;
                }
            }
            else if (error.InnerException != null)
            {
                if (returnWithStackTrace)
                {
                    return error.InnerException.ToString();
                }
                else
                {
                    return error.InnerException.Message;
                }
            }
            else
            {
                if (returnWithStackTrace)
                {
                    return error.ToString();
                }
                else
                {
                    return error.Message;
                }
            }
        }
    }
}