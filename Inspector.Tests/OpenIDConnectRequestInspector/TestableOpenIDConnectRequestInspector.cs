namespace OpenIDConnect.Inspector.Tests.OpenIDConnectRequestInspector
{
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using Controls;

    /// <summary>
    /// Testable implementation of OpenIDConnectRequestInspector.
    /// </summary>
    public class TestableOpenIDConnectRequestInspector : OpenIDConnect.Inspector.OpenIDConnectRequestInspector
    {
        /// <summary>
        /// Wrapper of the inner control used by the test runtime.
        /// </summary>
        public TabPage TabPage
        {
            get { return this.tabPage; }
        }

        /// <summary>
        /// Wrapper for the test runtime to call to AssignSession(oSession) using the specified SAZ archive.
        /// </summary>
        public void AssignSession(string filename)
        {
            var session = this.LoadFirstSessionOnly(filename);
            this.AssignSession(session);
        }

        /// <summary>
        /// Wrapper for the test runtime to call to ScoreForSession(oSession) using the specified SAZ archive.
        /// </summary>
        public int ScoreForSession(string filename)
        {
            var session = this.LoadFirstSessionOnly(filename);
            return this.ScoreForSession(session);
        }

        /// <summary>
        /// Loads only the very first session from the SAZ archive specified.
        /// </summary>
        private Fiddler.Session LoadFirstSessionOnly(string filename)
        {
            var resolver = new TestDependencyResolver();
            var provider = resolver.GetService<Fiddler.ISAZProvider>();
            var reader = provider.LoadSAZ(filename);

            var firstMarker = reader.GetRequestFileList().First();
            var sessionRequest = reader.GetFileBytes(firstMarker);

            var secondMarker = firstMarker.AsResponseMarker();
            var sessionResponse = reader.GetFileBytes(secondMarker);

            var thirdMarker = firstMarker.AsMetadataMarker();
            var sessionMetadata = reader.GetFileStream(thirdMarker);

            return this.InitSession(sessionRequest, sessionResponse, sessionMetadata);
        }

        /// <summary>
        /// Initializes an instance of session object using the request, response and metadata specified.
        /// </summary>
        internal Fiddler.Session InitSession(byte[] request, byte[] response, Stream metadata)
        {
            Fiddler.SessionFlags oAdditionalFlags = Fiddler.SessionFlags.LoadedFromSAZ;
            Fiddler.Session oS = new Fiddler.Session(request, response);
            if (metadata != null)
            {
                oS.LoadMetadata(metadata);
            }
            if (oS.oFlags.ContainsKey("ui-hide") && ((oAdditionalFlags & Fiddler.SessionFlags.LoadedFromSAZ) == Fiddler.SessionFlags.LoadedFromSAZ))
            {
                oS.oFlags["ui-strikeout"] = string.Format("Was ui-hide='{0}'", oS.oFlags["ui-hide"]);
                oS.oFlags.Remove("ui-hide");
            }

            return oS;
        }
    }
}