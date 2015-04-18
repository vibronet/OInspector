namespace jwt.cleanpii
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    class Program
    {
        private const string emptyGuidString = "00000000-0000-0000-0000-000000000000";
        private const string issuer = "https://sts.windows.net/" + emptyGuidString;

        private static Dictionary<string, string> map = new Dictionary<string, string>()
        {
            { "aud", emptyGuidString },
            { "unique_name", "live.com#jdoe@live.com" },
            { "email", "jdoe@live.com" },
            { "given_name", "John" },
            { "family_name", "Doe" },
            { "altsecid", "1:live.com:0000000000000000" },
            { "tid", emptyGuidString },
            { "iss", issuer }
        };

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