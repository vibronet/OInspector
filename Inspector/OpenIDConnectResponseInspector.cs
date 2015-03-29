namespace OpenIDConnect.Inspector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Fiddler;
    using Controls;

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
            base.AssignSession(oSession);

            if (this.ScoreForSession(oSession) == OidcSessionScore)
            {
                this.EnsureReady();
                this.DisplaySessionContent(oSession);
            }
        }

        private void DisplaySessionContent(Session oSession)
        {
            
        }

        private void EnsureReady()
        {
            if (this.gridView == null)
            {
                this.gridView = new WebFormEditor();
            }
        }

        public override int ScoreForContentType(string sMIMEType)
        {
            return base.ScoreForContentType(sMIMEType);
        }

        /// <summary>
        /// Scores how relevant the specified session is for the given inspector.
        /// </summary>
        public override int ScoreForSession(Session oSession)
        {
            if (oSession.IsOidcSession() || oSession.IsAuthorizationCodeResponse())
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

        public void Clear()
        {
            throw new NotImplementedException();
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
