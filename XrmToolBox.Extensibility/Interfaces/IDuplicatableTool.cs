namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IDuplicatableTool
    {
        /// <summary>
        /// Apply state retrieved from another tool instance to the current tool
        /// </summary>
        /// <param name="state">State to apply</param>
        void ApplyState(object state);

        /// <summary>
        /// Get the state of the current tool
        /// </summary>
        /// <returns></returns>
        object GetState();
    }
}