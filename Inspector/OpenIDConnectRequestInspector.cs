namespace OpenIDConnect.Inspector
{
    using System.Collections.Specialized;
    using System.Windows.Forms;
    using Controls;

    public class OpenIDConnectRequestInspector : Standard.RequestWebForm
    {
        protected TabPage tabPage;

        /// <summary>
        /// Initializes a specified tab with the layout and controls.
        /// </summary>
        public override void AddToTab(TabPage o)
        {
            base.AddToTab(o);

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
            base.AssignSession(oSession);
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
    }
}