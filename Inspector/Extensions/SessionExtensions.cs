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

        public static NameValueCollection GetQueryString(this Session oSession)
        {
            var indexOf = oSession.PathAndQuery.IndexOf("?") + 1;
            var queryString = oSession.PathAndQuery.Substring(indexOf);
            return Utilities.ParseQueryString(queryString);
        }

        public static bool IsAuthorizationCodeResponse(this Session oSession)
        {
            return oSession.utilFindInResponse("form method=\"POST\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"code\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"id_token\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"state\"", bCaseSensitive: false) > -1
                && oSession.utilFindInResponse("name=\"session_state\"", bCaseSensitive: false) > -1;
        }

        /// <summary>
        /// Gets whether the specified session is marked as OpenID Connect session or not.
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
    }
}
