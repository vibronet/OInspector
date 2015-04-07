namespace OpenIDConnect.Inspector.Tests.ResponseInspector
{
    using System.Collections.Specialized;
    using Common;
    using Fiddler;
    using OpenIDConnectRequestInspector;

    /// <summary>
    /// Testable implementation of OpenIDConnectResponseInspector.
    /// </summary>
    public class TestableResponseInspector : OpenIDConnectResponseInspector
    {
        /// <summary>
        /// Gets the session being inspected during the current test run.
        /// </summary>
        public Session Session { get; private set; }

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
            this.SimulateInspectorsPipeline(this.Session);
            return base.ScoreForSession(this.Session);
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

        /// <summary>
        /// Simulates inspectors pipeline to process the session in cooperation with the request inspector.
        /// </summary>
        private void SimulateInspectorsPipeline(Session session)
        {
            var inspector = new TestableOpenIDConnectRequestInspector();
            inspector.ScoreForSession(session);
        }
    }
}
