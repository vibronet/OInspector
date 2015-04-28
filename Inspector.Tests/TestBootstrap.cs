namespace OpenIDConnect.Inspector.Tests
{
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Workaround to setup some globally available things, like license context.
    /// </summary>
    [TestClass]
    public class TestBootstrap
    {
        /// <summary>
        /// Am empty test method to simulate testing for the sake of exercising automated deployment featured by MSTest.exe.
        /// </summary>
        [TestMethod, DeploymentItem(path: @"testSamples\", outputDirectory: "testSamples")]
        public void testSamples() { }

        /// <summary>
        /// We use this workaround to handle one-time initialization needs of the test project.
        /// </summary>
        [AssemblyInitialize]
        public static void GlobalInitialize(TestContext testContext)
        {
            InitializeLicenseContext();
        }

        /// <summary>
        /// Initializes proper license context to be used in the test runtime, which is enforced by Xceed components that Fiddler uses to save captured session(s) into files.
        /// </summary>
        private static void InitializeLicenseContext()
        {
            // NOTE: There is no dedicated context for test runtime, thus we resort to use design-time license context,
            // since having ability to use actual sessions captured in Fiddler drastically improves:
            // - unit tests maintanability (no need to use adhoc strings to mimic request/response for any given ssession);
            // - unit tests quality (real content is used against the code-under-test);
            LicenseManager.CurrentContext = new DesigntimeLicenseContext();
        }
    }
}