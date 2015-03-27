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
        /// <summary>
        /// Validates whether the simple text view control's text property has been reset to its default state.
        /// </summary>
        [TestMethod]
        public void ShouldCleanupAllText()
        {
            // Arrange
            var expected = "";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableOpenIDConnectRequestInspector> inspectorSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(@".\testSamples\loginRequest.saz");
            inspector.Clear();
            inspectorSpy(inspector);
        }
    }
}
