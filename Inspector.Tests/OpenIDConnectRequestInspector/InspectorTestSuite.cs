namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite to validate common traits of the inspector.
    /// </summary>
    [TestClass]
    public class InspectorTestSuite
    {
        /// <summary>
        /// Validates whether the tab has the expected name. 
        /// </summary>
        [TestMethod]
        public void ShouldSetTabNameAsExpected()
        {
            // Arrange
            var expected = "OIDC";
            var actual = default(string);

            // Act
            this.Act(tabSpy: (t) => actual = t.Text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TabPage> tabSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            var testDummy = new TabPage();
            inspector.AddToTab(testDummy);
            tabSpy(testDummy);
        }
    }
}
