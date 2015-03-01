namespace XrmToolBox.UnitTest
{
    /// <summary>The plugin manager specification fixture.</summary>
    public class PluginManagerSpecificationFixture
    {
        /// <summary>Gets or sets the under test.</summary>
        public PluginManager UnderTest { get; set; }

        /// <summary>The perform test setup.</summary>
        public void PerformTestSetup()
        {
            UnderTest = new PluginManager();
        }
    }
}