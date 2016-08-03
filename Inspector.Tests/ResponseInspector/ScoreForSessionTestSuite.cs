namespace OpenIDConnect.Inspector.Tests.ResponseInspector
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for ScoreForSession method of Inspector2 base class which response inspector is derived from.
    /// </summary>
    [TestClass]
    public class ScoreForSessionTestSuite
    {
        /// <summary>
        /// Validates whether the response inspector scores the authorization code response for confidential client as expected.
        /// </summary>
        [TestMethod]
        public void ScoreSession_AuthorizationCodeResponse_ConfidentialClient()
        {
            // Arrange
            var expected = 99;

            // Act
            var actual = this.Act(@".\testSamples\oidc-authorization-code-response-cc.saz");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the response inspector scores the authorization code response for native client as expected.
        /// </summary>
        [TestMethod]
        public void ScoreSession_AuthorizationCodeResponse_NativeClient()
        {
            // Arrange
            var expected = 99;

            // Act
            var actual = this.Act(@".\testSamples\oidc-authorization-code-response-nc.saz");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private int Act(string testSample)
        {
            var inspector = new TestableResponseInspector();
            return inspector.ScoreForSession(testSample);
        }
    }
}
