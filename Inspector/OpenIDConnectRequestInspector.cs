namespace OpenIDConnect.Inspector
{
    using System.Collections.Specialized;
    using Fiddler;

    public class OpenIDConnectRequestInspector : OidcInspector, IRequestInspector2
    {
        const int OidcSessionScore = 70;

        public OpenIDConnectRequestInspector()
            : base(title: "OIDC/OAuth")
        {
        }

        #region IRequestInspector2 Members

        public HTTPRequestHeaders headers { get; set; }

        #endregion

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Fiddler.Session oSession)
        {
            // Score ourselves at 70 when the path matches Authorize or Token endpoint
            if (oSession.PathAndQuery.IndexOf("/oauth2/authorize") > -1 ||
                oSession.PathAndQuery.IndexOf("/oauth2/token") > -1 ||
                oSession.HasBearerAuthorizationToken() ||
                oSession.IsAuthorizationCodeResponse())
            {
                oSession.MarkAsOidcSession();
                return OidcSessionScore;
            }

            return base.ScoreForSession(oSession);
        }

        protected override NameValueCollection ParseSession(Session oSession)
        {
            if (oSession.IsOidcSession() && oSession.PathAndQuery.IndexOf("/oauth2/authorize") > -1)
            {
                return oSession.GetQueryString();
            }
            else if (oSession.IsOidcSession() && 
                oSession.PathAndQuery.IndexOf("/oauth2/token") > -1 &&
                oSession.RequestMethod.OICEquals("POST"))
            {
                return oSession.GetRequestBody();
            }
            else if (oSession.IsIncomingAuthorizationCodeResponse())
            {
                return oSession.GetRequestBody();
            }
            else if (oSession.HasBearerAuthorizationToken())
            {
                return this.ParseAuthorizationHeader(oSession);
            }

            return null;
        }

        /// <summary>
        /// Gets a value indicating whether inspector can handle the given session.
        /// </summary>
        protected override bool CanHandleSession(Session oSession)
        {
            return this.ScoreForSession(oSession) == OidcSessionScore;
        }

        private NameValueCollection ParseAuthorizationHeader(Session oSession)
        {
            // Extracts the header's value without the token type (Bearer)
            var jwtEncodedString = oSession.oRequest.headers["Authorization"].Substring(7);

            // Let's do a trick - convert Authorization header into "bearer_token" entry in data source
            // to leverage token expansion feature in the base inspector.
            return new NameValueCollection()
            {
                { "bearer_token", jwtEncodedString }
            };
        }
    }
}