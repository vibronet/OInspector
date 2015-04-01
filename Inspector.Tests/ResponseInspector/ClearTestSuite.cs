namespace OpenIDConnect.Inspector.Tests.ResponseInspector
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test suite for Clear method of Inspector2 base class which response inspector is derived from.
    /// </summary>
    [TestClass]
    public class ClearTestSuite
    {
        /// <summary>
        /// Validates whether the state of grid view has been cleared out as expected.
        /// </summary>
        [TestMethod]
        public void ShouldCleanupAllGridRows()
        {
            // Arrange
            var expected = 0;
            var actual = -1;

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                actual = i.GetAllGridRowsCount();
            });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableResponseInspector> inspectorSpy)
        {
            var inspector = new TestableResponseInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(@".\testSamples\oidc-authorization-code-response.saz");
            inspector.Clear();
            inspectorSpy(inspector);
        }
    }
}
