using System.Linq;

namespace UserRepository
{
    public class EmailNormalizer
    {
        private const string NormalizedGmailDomain = "gmail.com";
        private static readonly string[] GmailDomains = new[] { NormalizedGmailDomain, "googlemail.com" };

        private readonly StringNormalizer _stringNormalizer;

        public EmailNormalizer(StringNormalizer stringNormalizer)
        {
            _stringNormalizer = stringNormalizer;
        }

        public string Normalize(string email)
        {
            var normalizedEmail = _stringNormalizer.NormalizeUnicodeLowercase(email);

            (string normalizedEmailUserName, string normalizedEmailDomain) = SplitEmail(normalizedEmail);

            if (IsGmail(normalizedEmailDomain))
            {
                normalizedEmailDomain = NormalizedGmailDomain;
                normalizedEmailUserName = NormalizeGmailUserName(normalizedEmailUserName);
            }

            return $"{normalizedEmailUserName}@{normalizedEmailDomain}";
        }

        private string NormalizeGmailUserName(string normalizedEmailUserName)
        {
            return RemoveLabelsInGmailUserName(
                RemoveDotsInGmailUserName(normalizedEmailUserName));
        }

        private string RemoveDotsInGmailUserName(string normalizedEmailUserName)
        {
            return normalizedEmailUserName.Replace(".", "");
        }

        private string RemoveLabelsInGmailUserName(string normalizedEmailUserName)
        {
            return new string(normalizedEmailUserName.TakeWhile(c => c != '+').ToArray());
        }

        private (string, string) SplitEmail(string email)
        {
            var parts = email.Split("@");
            if (parts.Length != 2)
                throw new UserRepositoryException("Invalid email");

            var emailUserName = parts[0];
            var emailDomain = parts[1];

            return (emailUserName, emailDomain);
        }

        private bool IsGmail(string normalizedEmailDomain)
        {
            return GmailDomains.Contains(normalizedEmailDomain);
        }
    }
}