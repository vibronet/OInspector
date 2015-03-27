namespace OpenIDConnect.Inspector
{
    using System.Collections.Specialized;
    using System.Windows.Forms;
    using Controls;

    public class OpenIDConnectRequestInspector : Fiddler.Inspector2, Fiddler.IRequestInspector2, Fiddler.IBaseInspector2
    {
        protected SimpleTextView textView;
        protected TabPage tabPage;

        public override void AddToTab(TabPage o)
        {
            this.tabPage = o;
            this.tabPage.Text = "OIDC";
        }

        public override int GetOrder()
        {
            return -1;
        }

        /// <summary>
        /// Assigns the specified session to the inspector.
        /// </summary>
        public override void AssignSession(Fiddler.Session oSession)
        {
            base.AssignSession(oSession);
            this.EnsureReady();
            this.Clear();
            this.ProcessSession(oSession);
        }

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Fiddler.Session oSession)
        {
            // Score ourselves at 70 when the path matches Authorize endpoint
            if (oSession.PathAndQuery.IndexOf("/oauth2/authorize") > -1)
            {
                return 70;
            }

            return base.ScoreForSession(oSession);
        }

        protected virtual void EnsureReady()
        {
            if (this.textView == null)
            {
                this.textView = new SimpleTextView();
                this.tabPage.Controls.Add(this.textView);
            }
        }

        private void ProcessSession(Fiddler.Session oSession)
        {
            var indexOf = oSession.PathAndQuery.IndexOf("?") + 1;
            var queryString = oSession.PathAndQuery.Substring(indexOf);
            var dictionary = Fiddler.Utilities.ParseQueryString(queryString);

            this.AppendLineSafely(dictionary, "client_id");
            this.AppendLineSafely(dictionary, "resource");
            this.AppendLineSafely(dictionary, "redirect_uri");
            this.AppendLineSafely(dictionary, "response_mode");
            this.AppendLineSafely(dictionary, "response_type");
            this.AppendLineSafely(dictionary, "scope");
            this.AppendLineSafely(dictionary, "site_id");
            this.AppendLineSafely(dictionary, "state");
            this.AppendLineSafely(dictionary, "nonce");
        }

        private void AppendLineSafely(NameValueCollection queryString, string key)
        {
            var value = queryString[key];
            if (!string.IsNullOrWhiteSpace(value))
            {
                this.textView.AppendLine(string.Format("{0} => {1}", key, value));
            }
        }

        #region IRequestInspector2 Members

        public Fiddler.HTTPRequestHeaders headers
        {
            get;
            set;
        }

        #endregion

        #region IBaseInspector2 Members

        public bool bDirty
        {
            get { return false; }
        }

        public bool bReadOnly
        {
            get { return true; }
            set { }
        }

        public byte[] body { get; set; }

        public void Clear()
        {
            // Wipe out all existing text
            this.textView.ResetText();
        }

        #endregion
    }
}