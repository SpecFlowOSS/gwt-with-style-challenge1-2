using System.Collections.Generic;

namespace UserRepository.Specs
{
    public class UserRepositoryDriver
    {
        private readonly UserRepositoryStore _userRepositoryStore;
        private readonly UserRepository _userRepository;

        public UserRepositoryDriver(UserRepositoryStore userRepositoryStore, UserRepository userRepository)
        {
            _userRepositoryStore = userRepositoryStore;
            _userRepository = userRepository;
        }

        public void CreateUsers(IEnumerable<User> users)
        {
            _userRepositoryStore.AddUsers(users);
        }

        public void RegisterUser(string personalName, string userName, string email)
        {
            _userRepository.RegisterUser(personalName, userName, email);
        }

        public List<User> GetAllUsers()
        {
            return _userRepositoryStore.GetUsers();
        }
    }
}
