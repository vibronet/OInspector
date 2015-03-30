namespace OpenIDConnect.Inspector.Tests.Common
{
    using System.IO;
    using System.Linq;
    using Fiddler;

    /// <summary>
    /// Session loader utility class.
    /// </summary>
    public class SessionLoader
    {
        /// <summary>
        /// Loads only the very first session from the SAZ archive specified.
        /// </summary>
        public Session LoadFirstSessionOnly(string filename)
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
        private Session InitSession(byte[] request, byte[] response, Stream metadata)
        {
            SessionFlags oAdditionalFlags = Fiddler.SessionFlags.LoadedFromSAZ;
            Session oS = new Session(request, response);
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
