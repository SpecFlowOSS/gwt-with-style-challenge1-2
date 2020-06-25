using System;
using System.Runtime.Serialization;

namespace UserRepository
{
    [Serializable]
    public class UserRepositoryException : Exception
    {

        public UserRepositoryException()
        {
        }

        public UserRepositoryException(string message) : base(message)
        {
        }

        public UserRepositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UserRepositoryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
