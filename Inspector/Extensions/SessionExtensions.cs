namespace OpenIDConnect.Inspector
{
    using System.Collections.Specialized;
    using Fiddler;

    /// <summary>
    /// Session object extension methods.
    /// </summary>
    public static class SessionExtensions
    {
        const string OidcFlag = "oidc";

        /// <summary>
        /// Gets a value indicating whether the given session is an authorization grant response or not.
        /// </summary>
        public static bool IsAuthorizationGrantResponse(this Session oSession)
        {
            return oSession.utilFindInResponse("\"token_type\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("\"access_token\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("\"refresh_token\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("\"resource\"", bCaseSensitive: false) > -1;
        }

        /// <summary>
        /// Gets query string as name/value collection from the given session's url.
        /// </summary>
        public static NameValueCollection GetQueryString(this Session oSession)
        {
            return ParseQueryString(oSession.PathAndQuery);
        }

        /// <summary>
        /// Gets query as name/value string from location HTTP header.
        /// </summary>
        public static NameValueCollection GetLocationQueryString(this Session oSession)
        {
            return ParseQueryString(oSession.GetRedirectTargetURL());
        }

        /// <summary>
        /// Gets a value indicating whether the specified session is an authorization code response for either a native client or confidential client.
        /// </summary>
        public static bool IsAuthorizationCodeResponse(this Session oSession)
        {
            return IsAuthorizationCodeResponse_ConfidentialClient(oSession) ||
                IsAuthorizationCodeResponse_NativeClient(oSession);
        }

        /// <summary>
        /// Gets a value indicating whether the specified session is an authorization code response for a native client or not.
        /// </summary>
        public static bool IsAuthorizationCodeResponse_NativeClient(this Session oSession)
        {
            var urlString = oSession.GetRedirectTargetURL();
            if (string.IsNullOrWhiteSpace(urlString))
            {
                return false;
            }

            return urlString.OICStartsWith("urn:ietf:wg:oauth:2.0:oob?")
                && urlString.OICContains("code=")
                && urlString.OICContains("session_state=");
        }

        /// <summary>
        /// Gets a value indicating whether the specified session is an authorization code response for a confidential client or not.
        /// </summary>
        public static bool IsAuthorizationCodeResponse_ConfidentialClient(this Session oSession)
        {
            return oSession.utilFindInResponse("form method=\"POST\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"code\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"id_token\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"state\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"session_state\"", bCaseSensitive: false) > -1;
        }

        /// <summary>
        /// Gets a value indicating whether the specified session is marked as OpenID Connect session or not.
        /// </summary>
        public static bool IsOidcSession(this Session oSession)
        {
            return !string.IsNullOrWhiteSpace(oSession[OidcFlag]);
        }

        /// <summary>
        /// Marks the specified session as an OpenID Connection session.
        /// </summary>
        public static void MarkAsOidcSession(this Session oSession)
        {
            oSession[OidcFlag] = "+";
        }

        private static NameValueCollection ParseQueryString(string urlString)
        {
            var indexOf = urlString.IndexOf("?") + 1;
            var queryString = urlString.Substring(indexOf);
            return Utilities.ParseQueryString(queryString);
        }
    }
}
