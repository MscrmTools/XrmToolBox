using System;
using System.ServiceModel;

namespace McTools.Xrm.Connection
{
    public class CrmExceptionHelper
    {
        public static string GetErrorMessage(Exception error, bool returnWithStackTrace)
        {
            if (error.InnerException is FaultException)
            {
                if (returnWithStackTrace)
                {
                    return ((FaultException)error.InnerException).ToString();
                }
                else
                {
                    return ((FaultException)error.InnerException).Message;
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