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
        private readonly IAppLogger<CustomerApplication> _appLogger;
        public CustomerApplication(ICustomersDomain customersDomain, IMapper mapper,
             IAppLogger<CustomerApplication> appLogger)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;
            _appLogger = appLogger;
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

        public Response<bool> Delete(string customerId)
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

        public Response<CustomersDto> Get(string customerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                var cuestomer = _customersDomain.Get(customerId);
                response.Data = _mapper.Map<CustomersDto>(cuestomer);
                if (response.Data != null)
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


        public Response<IEnumerable<CustomersDto>> GetAll()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                var customers = _customersDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta  Exitosa";
                    _appLogger.LogInformation("Consulta  Exitosa");
                }
            }
            catch (Exception e)
            {

                response.Message = e.Message;
                _appLogger.LogError(e.Message);
            }
            return response;

        }
        #endregion

        #region Métodos Asincronos

        public async Task<Response<bool>> InsertAsync(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customersDto);
                response.Data = await _customersDomain.InsertAsync(customer);
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

        public async Task<Response<bool>> UpdateAsync(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customersDto);
                response.Data = await _customersDomain.UpdateAsync(customer);
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

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _customersDomain.DeleteAsync(customerId);
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

        public async Task<Response<CustomersDto>> GetAsync(string customerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                var cuestomer = await _customersDomain.GetAsync(customerId);
                response.Data = _mapper.Map<CustomersDto>(cuestomer);
                if (response.Data != null)
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

        public async Task<Response<IEnumerable<CustomersDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                var cuestomers = await _customersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(cuestomers);
                if (response.Data != null)
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


        #endregion




    }
}