using Paquigroup.Ecommerce.Domain.Entity;

namespace Paquigroup.Ecommerce.Domain.Interface
{
    public interface ICustomersDomain
    {
        #region Métodos Sincronos
        bool Insert(Customers customers);

        bool Update(Customers customers);

        bool Delete(string customerId);

        Customers Get(string customerId);

        IEnumerable<Customers> GetAll();
        #endregion

        #region Métodos Asincronos

        Task<bool> InsertAsync(Customers customers);

        Task<bool> UpdateAsync(Customers customers);

        Task<bool> DeleteAsync(string customerId);

        Task<Customers> GetAsync(string customerId);

        Task<IEnumerable<Customers>> GetAllAsync();

        #endregion
    }
}
