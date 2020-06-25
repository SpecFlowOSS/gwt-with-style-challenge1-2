using System;
using System.Linq;

namespace UserRepository
{
    public class UserRepository
    {
        private readonly UserRepositoryStore _userRepositoryStore;

        public UserRepository(UserRepositoryStore userRepositoryStore)
        {
            _userRepositoryStore = userRepositoryStore;
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
            if (_userRepositoryStore.GetUsers()
                .Any(u => string.Equals(u.UserName, userName, StringComparison.InvariantCultureIgnoreCase)))
                throw new UserRepositoryException("Username taken");
        }
    }
}
