namespace OpenIDConnect.Inspector.Tests.Common
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Inspector tests assertion hub.
    /// </summary>
    public static class InspectorAssert
    {
        /// <summary>
        /// Asserts the only one instance of the specified substring exists in the actual text.
        /// </summary>
        public static void AssertOneInstanceOfSubstring(string text, string substring)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var counter = lines.Count(x => string.Equals(substring, x));
            if (counter > 1)
            {
                const string formatString = "Expected only '1' instance of '{0}' substring while found '{1}' instances of the specified substring in this text '{2}'";
                var message = string.Format(formatString, substring, counter, text);
                throw new AssertFailedException(message);
            }
        }
    }
}
