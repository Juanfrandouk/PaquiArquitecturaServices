using AutoMapper;
using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Application.Interface;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Transversal.Common;

namespace Paquigroup.Ecommerce.Application.Main
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;

        public UserApplication(IMapper mapper, IUserDomain userDomain)
        {
            _mapper = mapper;
            _userDomain = userDomain;

        }

        public Response<UserDto> Authenticate(string username, string password)
        {
            var response = new Response<UserDto>();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Parametros no pueden ser vacios";
                return response;
            }
            try
            {
                var user = _userDomain.Authenticate(username, password);
                response.Data = _mapper.Map<UserDto>(user);
                response.IsSuccess = true;
                response.Message = "Autenticacion !!!";
            }
            catch (InvalidDataException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";

            }
            catch (Exception e)
            {

                response.Message = e.Message;

            }
            return response;
        }
    }
}
