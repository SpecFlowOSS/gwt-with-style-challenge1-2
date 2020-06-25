using System;
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
                .Any(u => string.Equals(NormalizeUserName(u.UserName), normalizedUserName, StringComparison.InvariantCultureIgnoreCase)))
                throw new UserRepositoryException("Username taken");
        }

        private string NormalizeUserName(string userName)
        {
            return _stringNormalizer.Normalize(userName);
        }
    }
}
