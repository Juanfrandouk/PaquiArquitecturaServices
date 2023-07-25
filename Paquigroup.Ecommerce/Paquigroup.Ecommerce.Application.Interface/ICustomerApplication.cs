using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Transversal.Common;

namespace Paquigroup.Ecommerce.Application.Interface
{
    public interface ICustomerApplication
    {
        #region Métodos Sincronos
        Response<bool> Insert(CustomersDto customersDto);

        Response<bool> Update(CustomersDto customersDto);

        Response<bool> Delete(string customerId);

        Response<CustomersDto> Get(string customerId);

        Response<IEnumerable<CustomersDto>> GetAll();
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
