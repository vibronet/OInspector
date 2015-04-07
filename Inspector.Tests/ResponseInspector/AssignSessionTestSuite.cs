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
        /// Test sample capturing attributes of the confidential client response type
        /// </summary>
        const string testSampleCc = @".\testSamples\oidc-authorization-code-response-cc.saz";

        /// <summary>
        /// Test sample capturing attributes of the native client response type
        /// </summary>
        const string testSampleNc = @".\testSamples\oidc-authorization-code-response-nc.saz";

        /// <summary>
        /// Test sample capturing attributes of the native client response type
        /// </summary>
        const string testSampleAuthorizationGrantNc = @".\testSamples\oidc-authorization-code-grant-nc.saz";

        /// <summary>
        /// Validates whether the grid view contains expected access_token value with native client response type.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedAccessToken_NativeClient()
        {
            // Arrange
            var expected = "test_jwt_access_token_string_goes_here";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["access_token"];
            }, testSample: testSampleAuthorizationGrantNc);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected session_state value with native client response type.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedSessionState_NativeClient()
        {
            // Arrange
            var expected = "2f7e4213-dfa1-4964-840e-90f93f6b844d";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["session_state"];
            }, testSample: testSampleNc);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected session_state value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedSessionState_ConfidentialClient()
        {
            // Arrange
            var expected = "350a0b6a-181a-472d-b32a-38e1183e85b7";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["session_state"];
            }, testSample: testSampleCc);

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
                i.AssignSession(testSampleCc);
                actual = i.GetAllGridRowsCount();
            }, testSample: testSampleCc);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableResponseInspector> inspectorSpy, string testSample)
        {
            var inspector = new TestableResponseInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(testSample);
            inspectorSpy(inspector);
        }
    }
}
