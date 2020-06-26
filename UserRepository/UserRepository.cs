using System;
using System.Linq;

namespace UserRepository
{
    public class UserRepository
    {
        private readonly UserRepositoryStore _userRepositoryStore;
        private readonly StringNormalizer _stringNormalizer;
        private readonly EmailNormalizer _emailNormalizer;

        public UserRepository(UserRepositoryStore userRepositoryStore, StringNormalizer stringNormalizer, EmailNormalizer emailNormalizer)
        {
            _userRepositoryStore = userRepositoryStore;
            _stringNormalizer = stringNormalizer;
            _emailNormalizer = emailNormalizer;
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
            return _emailNormalizer.Normalize(email);
        }
    }
}
