using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public enum RightOrLeft
    {
        Left,
        Right,
        Bottom
    }

    public interface ICompanion
    {
        RightOrLeft GetPosition();
    }
}