using System.Reflection;

public class LogHelper {
    public static string GetFullMethodName(MethodBase methodBase)
    {
        string operationName = "";

        if (methodBase != null)
        {
            operationName = methodBase.DeclaringType != null ?
                $"{methodBase.DeclaringType.FullName}.{methodBase.Name}" :
                $"{methodBase.Name}";
        }

        return operationName;
    }

    public static string GetMethodName(MethodBase methodBase)
    {
        string methodName = "";

        if (methodBase != null)
            methodName = methodBase.Name;

        return methodName;
    }
}