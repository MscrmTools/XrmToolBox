using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ExceptionHelper
{
    public static AiException GetAiException(Exception e)
    {
        AiException aiException = new AiException
        {
            Id = e.HResult,
            TypeName = e.GetType().Name,
            Message = e.Message,
            HasFullStack = !string.IsNullOrEmpty(e.StackTrace)
        };

        if (aiException.HasFullStack)
            aiException.ParsedStacks = new List<AiParsedStack>();

        Exception currentException = e;
        while (currentException != null)
        {
            AiParsedStack parsedStack = ParseStackTrace(e);
            aiException.ParsedStacks.Add(parsedStack);

            currentException = currentException.InnerException;
        }

        return aiException;
    }

    private static AiParsedStack ParseStackTrace(Exception e)
    {
        StackTrace stackTrace = new StackTrace(e);
        StackFrame stackFrame = stackTrace.GetFrame(0);
        AiParsedStack aiParsedStack = new AiParsedStack

        {
            Method = stackFrame.GetMethod().Name,
            FileName = stackFrame.GetFileName(),
            Line = stackFrame.GetFileLineNumber()
        };

        return aiParsedStack;
    }
}