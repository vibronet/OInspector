namespace OpenIDConnect.Inspector
{
    using Fiddler;

    /// <summary>
    /// Session object extension methods.
    /// </summary>
    public static class SessionExtensions
    {
        const string OidcFlag = "oidc";

        public static bool IsAuthorizationCodeResponse(this Session oSession)
        {
            return true;
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
