namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for ScoreForSession method of Inspector2 base class which request inspector is derived from.
    /// </summary>
    [TestClass]
    public class ScoreForSessionTestSuite
    {
        private const string testSampleAuthorizationCodeRequest = @".\testSamples\oidc-authorization-code-request.saz";
        private const string testSampleOAuthWebApiPlusJwt = @".\testSamples\oauth-jwt-webapi-request.saz";

        /// <summary>
        /// Validates whether score returned matches expectations.
        /// </summary>
        [TestMethod]
        public void ShouldReturnExpectedScoreForSessionWithBearerToken()
        {
            // Arrange
            var expected = 70;

            // Act
            var actual = this.Act(inspectorSpy: (i) => { }, testSample: testSampleOAuthWebApiPlusJwt);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether a special flag is set for the session object.
        /// </summary>
        [TestMethod]
        public void ShouldMarkSessionAsOidcSession()
        {
            // Arrange
            var expected = true;
            var actual = false;

            // Act
            this.Act(inspectorSpy: (i) => {
                actual = i.Session.IsOidcSession();
            }, testSample: testSampleAuthorizationCodeRequest);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether score returned matches expectations.
        /// </summary>
        [TestMethod]
        public void ShouldReturnExpectedScoreForOidcSession()
        {
            // Arrange
            var expected = 70;

            // Act
            var actual = this.Act(inspectorSpy: (i) => { }, testSample: testSampleAuthorizationCodeRequest);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private int Act(Action<TestableRequestInspector> inspectorSpy, string testSample)
        {
            var inspector = new TestableRequestInspector();
            var sessionScore = inspector.ScoreForSession(testSample);
            inspectorSpy(inspector);
            return sessionScore;
        }
    }
}
