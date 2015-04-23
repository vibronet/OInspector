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
        /// <summary>
        /// Test sample capturing attributes of the authorization code request.
        /// </summary>
        private const string testSampleAuthorizationCode = @".\testSamples\oidc-authorization-code-request.saz";

        /// <summary>
        /// Test sample capturing attributes of the OAuth (WebApi + JWT) request.
        /// </summary>
        private const string testSampleOAuthWebApiPlusJwt = @".\testSamples\oauth-jwt-webapi-request.saz";

        /// <summary>
        /// Test sample capturing attrubutes of an incoming authorizarion code response (eq. sent to an app via post).
        /// </summary>
        private const string testSampleIncomingAuthorizationCodeResponse = @".\testSamples\oidc-incoming-authorization-code-response.saz";

        /// <summary>
        /// Validates whether the grid view contains expected id_token.upn claim value of an incoming authorization code response.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedIdentityToken_UserPrincipalNameClaim_OfIncomingAuthorizationCodeResponse()
        {
            // Arrange
            var expected = "jdoe@live.com";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["id_token.upn"];
            }, testSample: testSampleIncomingAuthorizationCodeResponse);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected id_token.aud claim value of an incoming authorization code response.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedIdentityToken_AudienceClaim_OfIncomingAuthorizationCodeResponse()
        {
            // Arrange
            var expected = "00000000-0000-0000-0000-000000000000";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["id_token.aud"];
            }, testSample: testSampleIncomingAuthorizationCodeResponse);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected session_state value of an incoming authorization code response.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedSessionStateOfIncomingAuthorizationCodeResponse()
        {
            // Arrange
            var expected = "680fe215-68e6-4081-afd2-04a85e2c696f";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["session_state"];
            }, testSample: testSampleIncomingAuthorizationCodeResponse);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected code value of an incoming authorization code response.
        /// </summary>
        [TestMethod]
        public void ShouldShowExpectedCodeOfIncomingAuthorizationCodeResponse()
        {
            // Arrange
            var expected = "AAABAAAAvPM1KaPlrEqdFSBzjqfTGBBK7c6m";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["code"];
            }, testSample: testSampleIncomingAuthorizationCodeResponse);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected bearer_token.unique_name value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnExpectedBearerToken_UniqueNameClaim()
        {
            // Arrange
            var expected = "live.com#jdoe@live.com";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["bearer_token.unique_name"];
            }, testSample: testSampleOAuthWebApiPlusJwt);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Validates whether the grid view contains expected bearer_token.aud value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnExpectedBearerToken_AudienceClaim()
        {
            // Arrange
            var expected = "00000000-0000-0000-0000-000000000000";
            var actual = default(string);

            // Act
            this.Act(inspectorSpy: (i) =>
            {
                var gridRows = i.GetAllGridRows();
                actual = gridRows["bearer_token.aud"];
            }, testSample: testSampleOAuthWebApiPlusJwt);

            // Assert
            Assert.AreEqual(expected, actual);
        }

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