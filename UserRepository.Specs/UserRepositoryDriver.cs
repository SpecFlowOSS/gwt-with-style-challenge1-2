using System.Collections.Generic;

namespace UserRepository.Specs
{
    public class UserRepositoryDriver
    {
        private readonly UserRepositoryStore _userRepositoryStore;

        public UserRepositoryDriver(UserRepositoryStore userRepositoryStore)
        {
            _userRepositoryStore = userRepositoryStore;
        }

        public void CreateUsers(IEnumerable<User> users)
        {
            _userRepositoryStore.AddUsers(users);
        }
    }
}
