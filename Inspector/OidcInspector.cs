namespace OpenIDConnect.Inspector
{
    using System;
    using System.Collections.Specialized;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Windows.Forms;
    using Controls;
    using Fiddler;

    /// <summary>
    /// Base inspector that shares common functionality between request and response inspectors (there is a lot of it).
    /// </summary>
    public abstract class OidcInspector : Inspector2, IBaseInspector2
    {
        private readonly string title;

        protected TabPage tabPage;
        protected WebFormEditor gridView;

        /// <summary>
        /// Instantiates a new instance of the OidcInspector class.
        /// </summary>
        protected OidcInspector(string title)
        {
            this.title = title;
        }

        #region Inspector 2 Members

        /// <summary>
        /// Initializes a specified tab with the layout and controls.
        /// </summary>
        public override void AddToTab(TabPage o)
        {
            this.tabPage = o;
            this.tabPage.Text = title;
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

            if (this.CanHandleSession(oSession))
            {
                this.EnsureReady();
                this.BindSessionWithDataSource(oSession);
            }
        }

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

        /// <summary>
        /// Gets a value indicating whether inspector can handle the given session.
        /// </summary>
        protected abstract bool CanHandleSession(Session oSession);

        /// <summary>
        /// Gets an instance of name/value collection from the given session or null if session type/content is not recognized.
        /// </summary>
        protected abstract NameValueCollection ParseSession(Session oSession);

        /// <summary>
        /// Ensures that child controls have been instantiated and the inspector is ready to be displayed.
        /// </summary>
        private void EnsureReady()
        {
            if (this.gridView == null)
            {
                this.gridView = new WebFormEditor();
                this.tabPage.Controls.Add(this.gridView);
                this.gridView.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// Binds the session to its parsed content.
        /// </summary>
        private void BindSessionWithDataSource(Session oSession)
        {
            var dataSource = this.ParseSession(oSession) ?? new NameValueCollection();

            this.ExpandJwtTokenStringIfAny(dataSource);

            this.gridView.Append(dataSource);
        }

        /// <summary>
        /// Expands jwt token strings in the specified data source if any and removes the original token string entry.
        /// </summary>
        private void ExpandJwtTokenStringIfAny(NameValueCollection dataSource)
        {
            var filteredKeys = dataSource.AllKeys.Where(this.MatchTokenKeyName);
            foreach (var key in filteredKeys)
            {
                var jwtEncodedString = dataSource.Get(key);
                if (!string.IsNullOrWhiteSpace(jwtEncodedString))
                {
                    // Remove the original entry from the data source to avoid confusion and aid readability
                    dataSource.Remove(key);

                    try
                    {
                        var jwt = new JwtSecurityToken(jwtEncodedString);
                        foreach (var claim in jwt.Claims)
                        {
                            this.SafeAddClaimValue(dataSource, claim, key);
                        }
                    }
                    catch (Exception e)
                    {
                        // TODO: Write more details about the exception to the log without using ambient context & compromizing testability.
                        dataSource.Add("parse_error", e.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Safely adds the specified claim into the data source provided using the supplied format string as a prefix.
        /// </summary>
        private void SafeAddClaimValue(NameValueCollection dataSource, Claim claim, string formatString)
        {
            if (claim == null)
            {
                return;
            }

            var keyValue = GetHumanFriendlyClaimValue(claim);
            var keyName = string.Concat(formatString, ".", claim.Type);
            dataSource.Add(keyName, keyValue);
        }

        private string GetHumanFriendlyClaimValue(Claim claim)
        {
            if (MatchTimeSpecificClaim(claim.Type))
            {
                var reference = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                double totalSecondsExpiresIn = Convert.ToDouble(claim.Value);
                var time = (reference + TimeSpan.FromSeconds(totalSecondsExpiresIn)).ToLocalTime();
                return string.Format("{0} ({1})", claim.Value, time);
            }

            return claim.Value;
        }

        private bool MatchTimeSpecificClaim(string type)
        {
            return type.OICEquals("exp")
                || type.OICEquals("iat")
                || type.OICEquals("nbf");
        }

        /// <summary>
        /// Matches the specified key with token names supported.
        /// </summary>
        private bool MatchTokenKeyName(string keyName)
        {
            return keyName.OICEquals("id_token")
                || keyName.OICEquals("access_token")
                || keyName.OICEquals("bearer_token");
        }
    }
}
