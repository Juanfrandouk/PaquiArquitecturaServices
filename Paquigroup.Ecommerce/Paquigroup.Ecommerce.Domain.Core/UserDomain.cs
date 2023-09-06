using Paquigroup.Ecommerce.Domain.Entity;
using Paquigroup.Ecommerce.Domain.Interface;

namespace Paquigroup.Ecommerce.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Users Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username, password);
        }
    }
}
