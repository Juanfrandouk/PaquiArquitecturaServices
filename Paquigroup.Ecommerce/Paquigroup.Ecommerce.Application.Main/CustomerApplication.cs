using AutoMapper;
using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Application.Interface;
using Paquigroup.Ecommerce.Domain.Entity;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Transversal.Common;

namespace Paquigroup.Ecommerce.Application.Main
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomersDomain _customersDomain;
        private readonly IMapper _mapper;

        public CustomerApplication(ICustomersDomain customersDomain, IMapper mapper)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;
        }
        #region Métodos Sincronos
        public Response<bool> Insert(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customersDto);
                response.Data = _customersDomain.Insert(customer);
                if (response.Data == true)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso";
                }
            }
            catch (Exception e)
            {

                response.Message = e.Message;
            }
            return response;
        }

        public Response<bool> Update(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customersDto);
                response.Data = _customersDomain.Update(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion  Exitosa";
                }
            }
            catch (Exception e)
            {

                response.Message = e.Message;
            }
            return response;
        }

        Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = _customersDomain.Delete(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Borrado  Exitoso";
                }
            }
            catch (Exception e)
            {

                response.Message = e.Message;
            }
            return response;
        }

        Response<CustomersDto> Get(string customerId);

        IEnumerable<CustomersDto> GetAll();
        #endregion

        #region Métodos Asincronos

        Task<Response<bool>> InsertAsync(CustomersDto customers);

        Task<Response<bool>> UpdateAsync(CustomersDto customers);

        Task<Response<bool>> DeleteAsync(string customerId);

        Task<Response<CustomersDto>> GetAsync(string customerId);

        Task<Response<IEnumerable<CustomersDto>>> GetAllAsync();
        #endregion




    }
}