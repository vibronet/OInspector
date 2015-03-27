namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for ScoreForSession method of Inspector2 base class which inspector is derived from.
    /// </summary>
    [TestClass]
    public class ScoreForSessionTestSuite
    {
        [TestMethod]
        public void ShouldReturnExpectedScoreForOidcSession()
        {
            // Arrange
            var expected = 70;

            // Act
            var actual = this.Act();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private int Act()
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            return inspector.ScoreForSession(@".\testSamples\loginRequest.saz");
        }
    }
}
