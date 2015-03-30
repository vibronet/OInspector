namespace OpenIDConnect.Inspector.Tests.ResponseInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for AssignSession method of Inspector2 base class which response inspector is derived from.
    /// </summary>
    [TestClass]
    public class AssignSessionTestSuite
    {
        /// <summary>
        /// Validates whether the grid view contains expected session_state value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedSessionState()
        {
            // Arrange
            var expected = "350a0b6a-181a-472d-b32a-38e1183e85b7";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["session_state"];
            });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableResponseInspector> inspectorSpy)
        {
            var inspector = new TestableResponseInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(@".\testSamples\oidc-authorization-code-response.saz");
            inspectorSpy(inspector);
        }
    }
}
