namespace OpenIDConnect.Inspector
{
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

        protected virtual void EnsureReady()
        {
            if (this.textView == null)
            {
                this.textView = new SimpleTextView();
                this.tabPage.Controls.Add(this.textView);
            }
        }

        #region IRequestInspector2 Members

        public Fiddler.HTTPRequestHeaders headers
        {
            get { return null; }
            set { this.SetHeaders(value); }
        }

        private void SetHeaders(Fiddler.HTTPRequestHeaders value)
        {
            this.EnsureReady();
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
        }

        #endregion
    }
}