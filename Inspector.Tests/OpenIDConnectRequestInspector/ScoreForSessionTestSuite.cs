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
            this.Act(inspectorSpy: (i) => actual = i.Session.IsOidcSession());

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
            var actual = this.Act(inspectorSpy: (i) => { });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private int Act(Action<TestableOpenIDConnectRequestInspector> inspectorSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            var sessionScore = inspector.ScoreForSession(@".\testSamples\loginRequest.saz");
            inspectorSpy(inspector);
            return sessionScore;
        }
    }
}
