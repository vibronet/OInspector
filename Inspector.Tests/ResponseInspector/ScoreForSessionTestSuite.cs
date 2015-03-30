namespace OpenIDConnect.Inspector.Tests.ResponseInspector
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Common;
    using Fiddler;
    using OpenIDConnectRequestInspector;

    /// <summary>
    /// Test suite for ScoreForSession method of Inspector2 base class which response inspector is derived from.
    /// </summary>
    [TestClass]
    public class ScoreForSessionTestSuite
    {
        [TestMethod]
        public void ShouldReturnExpectedScoreForOidcSession()
        {
            // Arrange
            var expected = 99;

            // Act
            var actual = this.Act();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private int Act()
        {
            var inspector = new TestableResponseInspector();
            return inspector.ScoreForSession(@".\testSamples\loginRequest.saz");
        }
    }
}
