namespace XrmToolBox.UnitTest
{
    using DSmall.UnitTest.Core;
    using NUnit.Framework;

    /// <summary>The when plugin manager loads plugins successfully.</summary>
    [TestFixture]
    public class WhenPluginManagerLoadsPluginsSuccessfully : SpecificationBase
    {
        private PluginManagerSpecificationFixture testFixture;

        /// <summary>The should load dummy plugin correctly.</summary>
        [Test]
        public void ShouldLoadDummyPluginCorrectly()
        {
            Assert.IsTrue(testFixture.UnderTest.Plugins.Contains(typeof(DummyXrmToolBoxPlugin)));
        }

        /// <summary>The because of.</summary>
        protected override void BecauseOf()
        {
            base.BecauseOf();

            testFixture.UnderTest.LoadPlugins();
        }

        /// <summary>The context.</summary>
        protected override void Context()
        {
            base.Context();

            testFixture = new PluginManagerSpecificationFixture();
            testFixture.PerformTestSetup();
        }
    }
}
