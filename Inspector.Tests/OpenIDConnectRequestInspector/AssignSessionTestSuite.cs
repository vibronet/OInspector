namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIDConnect.Inspector.Controls;

    /// <summary>
    /// Test suite for AssignSession method of Inspector2 base class which inspector is derived from.
    /// </summary>
    [TestClass]
    public class AssignSessionTestSuite
    {
        private const string testSample = @".\testSamples\oidc-authorization-code-request.saz";

        private void Act(Action<TestableOpenIDConnectRequestInspector> inspectorSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(testSample);
            inspectorSpy(inspector);
        }
    }
}