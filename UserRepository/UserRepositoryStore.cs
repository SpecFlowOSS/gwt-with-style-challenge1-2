using System.Collections.Generic;

namespace UserRepository
{
    public class UserRepositoryStore
    {
        private readonly List<User> _users = new List<User>();

        public void AddUsers(IEnumerable<User> users)
        {
            _users.AddRange(users);
        }
    }
}
