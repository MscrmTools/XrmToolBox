using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public enum RightOrLeft
    {
        Left,
        Right
    }

    public interface ICompanion
    {
        RightOrLeft GetPosition();
    }
}