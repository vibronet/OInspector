namespace OpenIDConnect.Inspector
{
    using System.Collections.Specialized;
    using System.Windows.Forms;
    using Controls;
    using Fiddler;

    public class OpenIDConnectRequestInspector : Inspector2, IRequestInspector2, IBaseInspector2
    {
        const int OidcSessionScore = 70;
        protected TabPage tabPage;
        protected WebFormEditor gridView;

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
        public override void AssignSession(Fiddler.Session oSession)
        {
            this.Clear();
            base.AssignSession(oSession);

            if (this.ScoreForSession(oSession) == OidcSessionScore)
            {
                this.EnsureReady();
                this.DisplaySessionContent(oSession);
            }
        }

        private void DisplaySessionContent(Session oSession)
        {
            var dataSource = new NameValueCollection();

            if (oSession.IsOidcSession())
            {
                dataSource = oSession.GetQueryString();
            }

            this.gridView.Append(dataSource);
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

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Fiddler.Session oSession)
        {
            // Score ourselves at 70 when the path matches Authorize endpoint
            if (oSession.PathAndQuery.IndexOf("/oauth2/authorize") > -1)
            {
                oSession.MarkAsOidcSession();
                return OidcSessionScore;
            }

            return base.ScoreForSession(oSession);
        }

        #region IRequestInspector2 Members

        public HTTPRequestHeaders headers { get; set; }

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
    }
}