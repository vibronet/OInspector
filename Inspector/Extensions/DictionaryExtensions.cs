namespace OpenIDConnect.Inspector
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public static class DictionaryExtensions
    {
        public static NameValueCollection AsNameValueCollection(this Dictionary<string, string> input)
        {
            var output = new NameValueCollection();
            foreach (var item in input)
            {
                output.Add(item.Key, item.Value);
            }
            return output;
        }
    }
}
