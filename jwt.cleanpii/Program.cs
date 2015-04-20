namespace jwt.cleanpii
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Windows.Forms;
    using Newtonsoft.Json;

    class Program
    {
        private static readonly Dictionary<string, string> map;
        private static readonly string issuer;

        static Program()
        {
            issuer = ConfigurationManager.AppSettings["issuer"];
            map = JsonConvert.DeserializeObject<Dictionary<string, string>>(ConfigurationManager.AppSettings["map"]);
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Read the input from command-line or clipboard
            var jwtEncodedString = args.FirstOrDefault() ?? Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(jwtEncodedString))
            {
                return;
            }

            var input = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var claims = CleanupClaims(input.Claims);
            var payload = new JwtPayload(claims);
            var output = new JwtSecurityToken(input.Header, payload, input.RawHeader, payload.Base64UrlEncode(), input.RawSignature);

            var jwtEncodedOutputString = output.RawData;

            Clipboard.SetText(jwtEncodedOutputString);
            Console.WriteLine(jwtEncodedOutputString);
        }

        private static IEnumerable<Claim> CleanupClaims(IEnumerable<Claim> input)
        {
            var output = new List<Claim>();

            foreach (var claim in input)
            {
                output.Add(CloneClaim(claim));
            }

            return output;
        }

        private static Claim CloneClaim(Claim source)
        {
            var value = source.Value;
            if (map.ContainsKey(source.Type))
            {
                value = map[source.Type];
            }

            var clone = new Claim(type: source.Type, value: value, valueType: source.ValueType, issuer: issuer, originalIssuer: issuer);
            return clone;
        }

        private static bool StringEquals(string first, string second)
        {
            return string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
        }
    }
}