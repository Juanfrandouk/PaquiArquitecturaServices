using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Transversal.Common;

namespace Paquigroup.Ecommerce.Application.Interface
{
    public interface IUserApplication
    {
        Response<UserDto> Authenticate(string username, string password);

    }
}
