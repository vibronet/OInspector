namespace OpenIDConnect.Inspector.Tests
{
    /// <summary>
    /// Helper methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the given request marker into the corresponding response marker inside SAZ archive.
        /// </summary>
        public static string AsResponseMarker(this string marker)
        {
            return marker.Replace("_c.txt", "_s.txt");
        }

        /// <summary>
        /// Converts the given request marker into the corresponding metadata marker inside SAZ archive.
        /// </summary>
        public static string AsMetadataMarker(this string marker)
        {
            return marker.Replace("_c.txt", "_m.xml");
        }
    }
}
