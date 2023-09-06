using Paquigroup.Ecommerce.Domain.Entity;

namespace Paquigroup.Ecommerce.Domain.Interface
{
    public interface IUserDomain
    {
        Users Authenticate(string username, string password);
    }
}
