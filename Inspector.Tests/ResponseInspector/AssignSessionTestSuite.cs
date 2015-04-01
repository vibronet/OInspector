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
        const string testSample = @".\testSamples\oidc-authorization-code-response.saz";

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

        /// <summary>
        /// Validates whether AssignSession method takes care of wiping out the previous state.
        /// </summary>
        [TestMethod]
        public void ShouldWipeOutPreviousStateWhenAssignNewSession()
        {
            // Arrange
            var expected = 4;
            var actual = -1;

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                // Exercise the scenario again to compare the outcome
                i.AssignSession(testSample);
                actual = i.GetAllGridRowsCount();
            });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableResponseInspector> inspectorSpy)
        {
            var inspector = new TestableResponseInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(testSample);
            inspectorSpy(inspector);
        }
    }
}
