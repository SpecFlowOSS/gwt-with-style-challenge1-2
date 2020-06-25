using System;
using System.Collections.Generic;

namespace UserRepository.Specs
{
    public class UserRepositoryDriver
    {
        private readonly UserRepositoryStore _userRepositoryStore;
        private readonly UserRepository _userRepository;
        private UserRepositoryException _lastException = null;

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
            try
            {
                _lastException = null;
                _userRepository.RegisterUser(personalName, userName, email);
            }
            catch (UserRepositoryException error)
            {
                _lastException = error;
            }
        }

        public List<User> GetAllUsers()
        {
            return _userRepositoryStore.GetUsers();
        }

        public string RegistrationErrorMessage => _lastException?.Message;
    }
}
