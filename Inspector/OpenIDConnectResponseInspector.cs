namespace OpenIDConnect.Inspector
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Windows.Forms;
    using Controls;
    using Fiddler;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// OpenID Connect response inspector.
    /// </summary>
    public class OpenIDConnectResponseInspector : OidcInspector, IResponseInspector2
    {
        const int OidcSessionScore = 99;

        public OpenIDConnectResponseInspector()
            : base(title: "OIDC")
        {
        }

        #region IResponseInspector2 Members

        public HTTPResponseHeaders headers { get; set; }

        #endregion

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Session oSession)
        {
            if (oSession.IsOidcSession() ||
                oSession.IsAuthorizationCodeResponse() ||
                oSession.IsAuthorizationGrantResponse())
            {
                return OidcSessionScore;
            }

            return base.ScoreForSession(oSession);
        }

        /// <summary>
        /// Gets a value indicating whether inspector can handle the given session.
        /// </summary>
        protected override bool CanHandleSession(Session oSession)
        {
            return true;// this.ScoreForSession(oSession) == OidcSessionScore;
        }

        protected override NameValueCollection ParseSession(Session oSession)
        {
            if (oSession.IsAuthorizationCodeResponse_ConfidentialClient())
            {
                return this.ParseAuthorizationCodeResponse(oSession);
            }
            else if (oSession.IsAuthorizationCodeResponse_NativeClient())
            {
                return this.ParseAuthorizationCodeResponse_NativeClient(oSession);
            }
            else if (oSession.IsAuthorizationGrantResponse() || oSession.IsAuthorizationCodeResponse_IdentityServerClient())
            {
                return this.ParseAuthorizationGrantResponse(oSession);
            }
            else if (oSession?.oResponse?.headers.Any(h => h.Name == "Location") ?? false)
            {
                return this.ParseTokenResponse(oSession);
            }

            return null;
        }

        private NameValueCollection ParseAuthorizationGrantResponse(Session oSession)
        {
            var bodyString = oSession.GetResponseBodyAsString();
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(bodyString);
            return json.AsNameValueCollection();
        }

        private NameValueCollection ParseAuthorizationCodeResponse_NativeClient(Session oSession)
        {
            return oSession.GetLocationQueryString();
        }

        private NameValueCollection ParseAuthorizationCodeResponse(Session oSession)
        {
            var map = new NameValueCollection();
            var bodyString = oSession.GetResponseBodyAsString();
            var startInputOf = bodyString.IndexOf("<input");
            var endInputOf = bodyString.IndexOf("/>", startInputOf);

            while (startInputOf > -1 && endInputOf > -1)
            {
                var tagString = bodyString.Substring(startInputOf, (endInputOf + 2) - startInputOf);

                var nameOf = tagString.IndexOf(" name");
                if (nameOf == -1)
                {
                    // We're done with this form
                    break;
                }

                var startQuoteOf = tagString.IndexOf('"', nameOf) + 1;
                var endQuoteOf = tagString.IndexOf('"', startQuoteOf);

                // We've just got value of name sttribute
                var name = tagString.Substring(startQuoteOf, endQuoteOf - startQuoteOf);

                var valueOf = tagString.IndexOf(" value", endQuoteOf);
                startQuoteOf = tagString.IndexOf('"', valueOf) + 1;
                endQuoteOf = tagString.IndexOf('"', startQuoteOf);

                // We've just got value of value sttribute
                var value = tagString.Substring(startQuoteOf, endQuoteOf - startQuoteOf);

                map.Add(name, value);

                startInputOf = bodyString.IndexOf("<input", endInputOf);

                if (startInputOf == -1)
                {
                    // We're done with this form
                    break;
                }

                endInputOf = bodyString.IndexOf("/>", startInputOf);
            }

            return map;
        }

        private NameValueCollection ParseTokenResponse(Session oSession)
        {
            var map = new NameValueCollection();

            var location = oSession.oResponse.headers.First(h => h.Name == "Location").Value;

            if (location.ToLower().Contains("id_token="))
            {
                var url = new Uri(location);

                var query = url.Query;

                if (string.IsNullOrEmpty(query) && url.Fragment.StartsWith("#"))
                    query = url.Fragment.Substring(1);

                var prms = query.Split('&');

                prms.ToList().ForEach(prm =>
                {
                    var kv = prm.Split('=');

                    map.Add(kv[0], kv[1]);
                });
            }

            return map;
        }
    }
}
