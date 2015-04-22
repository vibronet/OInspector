namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for AssignSession method of Inspector2 base class which inspector is derived from.
    /// </summary>
    [TestClass]
    public class AssignSessionTestSuite
    {
        private const string testSampleAuthorizationCode = @".\testSamples\oidc-authorization-code-request.saz";

        /// <summary>
        /// Validates whether AssignSession method takes care of clearing out the previous state.
        /// </summary>
        [TestMethod]
        public void ShouldWipeOutPreviousStateWhenAssignNewSession()
        {
            // Arrange
            var expected = -1;
            var actual = -1;

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                // Save results from the first run
                expected = i.GetAllGridRowsCount();

                // Exercise the scenario again to compare the outcome
                i.AssignSession(testSampleAuthorizationCode);

                // Save results from the second run
                actual = i.GetAllGridRowsCount();
            }, testSample: testSampleAuthorizationCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected resource value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedResource()
        {
            // Arrange
            var expected = "https://management.core.windows.net/";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["resource"];
            }, testSample: testSampleAuthorizationCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected response_type value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedResponseType()
        {
            // Arrange
            var expected = "code id_token";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["response_type"];
            }, testSample: testSampleAuthorizationCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableRequestInspector> inspectorSpy, string testSample)
        {
            var inspector = new TestableRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(testSample);
            inspectorSpy(inspector);
        }
    }
}