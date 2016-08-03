namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System.Collections.Specialized;
    using System.Windows.Forms;
    using Common;
    using Fiddler;

    /// <summary>
    /// Testable implementation of OpenIDConnectRequestInspector.
    /// </summary>
    public class TestableRequestInspector : OpenIDConnect.Inspector.OpenIDConnectRequestInspector
    {
        /// <summary>
        /// Gets the session being inspected during the current test run.
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// Wrapper of the inner control used by the test runtime.
        /// </summary>
        public TabPage TabPage
        {
            get { return this.tabPage; }
        }

        /// <summary>
        /// Wrapper for the test runtime to get count of all grid rows.
        /// </summary>
        public int GetAllGridRowsCount()
        {
            return this.gridView.GetAllGridRowsCount();
        }

        /// <summary>
        /// Wrapper for the test runtime to get all grid rows as 'name => value' collection.
        /// </summary>
        public NameValueCollection GetAllGridRows()
        {
            if (this.gridView == null)
            {
                return new NameValueCollection();
            }

            return this.gridView.GetAllGridRows();
        }

        /// <summary>
        /// Wrapper for the test runtime to call to AssignSession(oSession) using the specified SAZ archive.
        /// </summary>
        public void AssignSession(string filename)
        {
            this.Session = this.LoadFirstSessionOnly(filename);
            this.AssignSession(this.Session);
        }

        /// <summary>
        /// Wrapper for the test runtime to call to ScoreForSession(oSession) using the specified SAZ archive.
        /// </summary>
        public int ScoreForSession(string filename)
        {
            this.Session = this.LoadFirstSessionOnly(filename);
            return this.ScoreForSession(this.Session);
        }

        /// <summary>
        /// Loads only the very first session from the SAZ archive specified.
        /// </summary>
        private Fiddler.Session LoadFirstSessionOnly(string filename)
        {
            var resolver = new TestDependencyResolver();
            var loader = resolver.GetService<SessionLoader>();
            return loader.LoadFirstSessionOnly(filename);
        }
    }
}