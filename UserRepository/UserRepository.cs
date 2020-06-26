using System.Linq;

namespace UserRepository
{
    public class UserRepository
    {
        private readonly UserRepositoryStore _userRepositoryStore;
        private readonly StringNormalizer _stringNormalizer;

        public UserRepository(UserRepositoryStore userRepositoryStore, StringNormalizer stringNormalizer)
        {
            _userRepositoryStore = userRepositoryStore;
            _stringNormalizer = stringNormalizer;
        }

        public void RegisterUser(string personalName, string userName, string email)
        {
            CheckForIdenticalUserName(userName);
            CheckForIdenticalEmail(email);

            var user = new User
            {
                PersonalName = personalName,
                UserName = userName,
                Email = email
            };

            _userRepositoryStore.AddUsers(new []{user});
        }

        private void CheckForIdenticalUserName(string userName)
        {
            var normalizedUserName = NormalizeUserName(userName);

            if (_userRepositoryStore.GetUsers()
                .Any(u => NormalizeUserName(u.UserName) == normalizedUserName)
                
            )
                throw new UserRepositoryException("Username taken");
        }

        private void CheckForIdenticalEmail(string email)
        {
            var normalizedEmail = NormalizeEmail(email);

            if (_userRepositoryStore.GetUsers()
                .Any(u => NormalizeEmail(u.Email) == normalizedEmail)
                
            )
                throw new UserRepositoryException("Email already registered");
        }

        private string NormalizeUserName(string userName)
        {
            return _stringNormalizer.NormalizeUnicodeLowercaseInterpunction(userName);
        }

        private string NormalizeEmail(string email)
        {
            var normalizedEmail = _stringNormalizer.NormalizeUnicodeLowercase(email);

            (string normalizedEmailUserName, string normalizedEmailDomain) = SplitEmail(normalizedEmail);

            if (IsGmail(normalizedEmailDomain))
            {
                normalizedEmailDomain = NormalizedGmailDomain;
            }

            return $"{normalizedEmailUserName}@{normalizedEmailDomain}";
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

        private const string NormalizedGmailDomain = "gmail.com";
        private readonly string[] GmailDomains = new[] {NormalizedGmailDomain, "googlemail.com"};
        private bool IsGmail(string normalizedEmailDomain)
        {
            return GmailDomains.Contains(normalizedEmailDomain);
        }
    }
}
