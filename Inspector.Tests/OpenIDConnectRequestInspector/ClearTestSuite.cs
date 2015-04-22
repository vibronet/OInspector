namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for Clear method of IBaseInspector2 interface which the inspector implements.
    /// </summary>
    [TestClass]
    public class ClearTestSuite
    {
        private void Act(Action<TestableRequestInspector> inspectorSpy)
        {
            var inspector = new TestableRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(@".\testSamples\oidc-authorization-code-request.saz");
            inspector.Clear();
            inspectorSpy(inspector);
        }
    }
}
