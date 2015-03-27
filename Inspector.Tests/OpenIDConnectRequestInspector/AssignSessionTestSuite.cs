namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System;
    using System.Windows.Forms;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIDConnect.Inspector.Controls;

    /// <summary>
    /// Test suite for AssignSession method of Inspector2 base class which inspector is derived from.
    /// </summary>
    [TestClass]
    public class AssignSessionTestSuite
    {
        private const string testSample = @".\testSamples\loginRequest.saz";

        /// <summary>
        /// Validates whether the simple text view contains an expected site_id value.
        /// </summary>
        [TestMethod]
        public void ShouldWipeOutTextViewWhenAssignedAnotherSession()
        {
            // Arrange
            var expected = "site_id => 501430";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                // Repeat the run with the very same sample to ensure the state is wiped out
                i.AssignSession(testSample);
                actual = i.TextView.Text;
            });

            // Assert
            InspectorAssert.AssertOneInstanceOfSubstring(substring: expected, text: actual);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected site_id value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedSiteId()
        {
            // Arrange
            var expected = "site_id => 501430";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected redirect_uri value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedRedirectUri()
        {
            // Arrange
            var expected = "redirect_uri => https://portal.azure.com/";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected client_id value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedClientId()
        {
            // Arrange
            var expected = "client_id => c44b4083-3bb0-49c1-b47d-974e53cbdf3c";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected nonce value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedNonce()
        {
            // Arrange
            var expected = "nonce => 635626483351462548.OWQ1MGNkNTItY2ZmOC00ZTYyLTljNmUtNmM2MDU4NzE5NDkxOTU2Mjg3ODctZGI3MC00NTA1LTg3MWItZTdkZjkyNmQ0ODgy";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected state value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedState()
        {
            // Arrange
            var expected = "state => OpenIdConnect.AuthenticationProperties=X4sOd_vuGjn93VYMt92C9eXtWkyOj56ax-Qg08jU8O6ctGqR1I0gR5V9El0LRYntBFCSonBJFu8QqcwnnCGmoCKNycSoweaGL8lmY3baTe68UY6c9Zy9NHBv0fe820KgJR7XbA";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected scope value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedScope()
        {
            // Arrange
            var expected = "scope => user_impersonation openid";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected response_type value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedResponseType()
        {
            // Arrange
            var expected = "response_type => code id_token";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected response_mode value.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedResponseMode()
        {
            // Arrange
            var expected = "response_mode => form_post";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view contains an expected resource identifier.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedResource()
        {
            // Arrange
            var expected = "resource => https://management.core.windows.net/";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView.Text);

            // Assert
            StringAssert.Contains(actual, expected);
        }

        /// <summary>
        /// Validates whether the simple text view control is instantiated.
        /// </summary>
        [TestMethod]
        public void ShouldInstantiateTextViewControl()
        {
            // Arrange
            var actual = default(SimpleTextView);

            // Act
            this.Act(inspectorSpy: (i) => actual = i.TextView);

            // Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Validates whether the simple text view control has been properly injected into the tab.
        /// </summary>
        [TestMethod]
        public void ShouldInjectTextViewIntoTabPage()
        {
            // Arrange
            var expected = default(SimpleTextView);
            var actual = default(SimpleTextView);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                expected = i.TextView;
                actual = i.TabPage.Controls[0] as SimpleTextView;
            });

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        private void Act(Action<TestableOpenIDConnectRequestInspector> inspectorSpy)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            inspector.AddToTab(new TabPage());
            inspector.AssignSession(testSample);
            inspectorSpy(inspector);
        }
    }
}