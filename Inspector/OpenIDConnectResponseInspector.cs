namespace OpenIDConnect.Inspector
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Windows.Forms;
    using Controls;
    using Fiddler;
    using Newtonsoft.Json;

    /// <summary>
    /// OpenID Connect response inspector.
    /// </summary>
    public class OpenIDConnectResponseInspector : Inspector2, IResponseInspector2, IBaseInspector2
    {
        const int OidcSessionScore = 99;
        protected TabPage tabPage;
        protected WebFormEditor gridView;

        #region Inspector 2 Members

        /// <summary>
        /// Initializes a specified tab with the layout and controls.
        /// </summary>
        public override void AddToTab(TabPage o)
        {
            this.tabPage = o;
            this.tabPage.Text = "OIDC";
        }

        /// <summary>
        /// Gets tab page order of the inspector.
        /// </summary>
        public override int GetOrder()
        {
            return -1;
        }

        /// <summary>
        /// Assigns the specified session to the inspector.
        /// </summary>
        public override void AssignSession(Session oSession)
        {
            this.Clear();
            base.AssignSession(oSession);

            if (this.ScoreForSession(oSession) == OidcSessionScore)
            {
                this.EnsureReady();
                this.DisplaySessionContent(oSession);
            }
        }

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Session oSession)
        {
            if (oSession.IsOidcSession()
                || oSession.IsAuthorizationCodeResponse()
                || oSession.IsAuthorizationGrantResponse())
            {
                return OidcSessionScore;
            }

            return base.ScoreForSession(oSession);
        }

        #endregion

        #region IResponseInspector2 Members

        public HTTPResponseHeaders headers { get; set; }

        #endregion

        #region IBaseInspector2 Members

        /// <summary>
        /// Wipes out state of all child controls.
        /// </summary>
        public void Clear()
        {
            if (this.gridView != null)
            {
                this.gridView.Clear();
            }
        }

        public bool bDirty
        {
            get { return false; }
        }

        public bool bReadOnly { get; set; }

        public byte[] body { get; set; }

        #endregion

        private void DisplaySessionContent(Session oSession)
        {
            var dataSource = new NameValueCollection();
            if (oSession.IsAuthorizationCodeResponse_ConfidentialClient())
            {
                dataSource = this.ParseAuthorizationCodeResponse(oSession);
            }
            else if (oSession.IsAuthorizationCodeResponse_NativeClient())
            {
                dataSource = this.ParseAuthorizationCodeResponse_NativeClient(oSession);
            }
            else if (oSession.IsAuthorizationGrantResponse())
            {
                dataSource = this.ParseAuthorizationGrantResponse(oSession);
            }

            this.ExpandJwtTokenStringIfAny(dataSource);

            this.gridView.Append(dataSource);
        }

        private void ExpandJwtTokenStringIfAny(NameValueCollection dataSource)
        {
            var jwtEncodedString = dataSource.Get("id_token");
            if (!string.IsNullOrWhiteSpace(jwtEncodedString))
            {
                dataSource.Remove("id_token");
                var jwt = new JwtSecurityToken(jwtEncodedString);
                foreach (var claim in jwt.Claims)
                {
                    this.SafeAddClaimValue(dataSource, claim, "id_token");
                }
            }
        }

        private void SafeAddClaimValue(NameValueCollection dataSource, Claim claim, string formatString)
        {
            if (claim == null)
            {
                return;
            }

            var keyName = string.Concat(formatString, ".", claim.Type);
            dataSource.Add(keyName, claim.Value);
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
                endInputOf = bodyString.IndexOf("/>", startInputOf);
            }

            return map;
        }

        private void EnsureReady()
        {
            if (this.gridView == null)
            {
                this.gridView = new WebFormEditor();
                this.tabPage.Controls.Add(this.gridView);
                this.gridView.Dock = DockStyle.Fill;
            }
        }
    }
}
