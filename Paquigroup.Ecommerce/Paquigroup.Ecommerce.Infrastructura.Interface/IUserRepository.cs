using Paquigroup.Ecommerce.Domain.Entity;

namespace Paquigroup.Ecommerce.Domain.Interface
{
    public interface IUserRepository
    {
        Users Authenticate(string username, string password);
    }
}
