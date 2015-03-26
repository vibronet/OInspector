namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIDConnect.Inspector.Controls;

    /// <summary>
    /// Test suite for header property of IRequestInspector2 interface which inspector implements.
    /// </summary>
    [TestClass]
    public class HeadersTestSuite
    {
        /// <summary>
        /// Validates whether the simple text view control is instantiated.
        /// </summary>
        [TestMethod]
        public void SetShouldInstantiateTextViewControl()
        {
            // Arrange
            var actual = default(SimpleTextView);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView);

            // Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Validates whether the simple text view control has been properly injected into the tab.
        /// </summary>
        [TestMethod]
        public void SetShouldInjectTextViewIntoTabPage()
        {
            // Arrange
            var expected = default(SimpleTextView);
            var actual = default(SimpleTextView);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                expected = i.TextView;
                actual = i.TabPage.Controls[0] as SimpleTextView;
            });

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableOpenIDConnectRequestInspector> inspectorSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(@".\testSamples\loginRequest.saz");
            inspectorSpy(inspector);
        }
    }
}