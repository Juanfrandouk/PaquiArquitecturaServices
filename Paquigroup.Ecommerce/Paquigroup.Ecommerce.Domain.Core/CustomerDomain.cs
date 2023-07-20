using Paquigroup.Ecommerce.Domain.Entity;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Infrastructura.Interface;

namespace Paquigroup.Ecommerce.Domain.Core
{
    public class CustomerDomain : ICustomersDomain
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerDomain(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region Métodos Sincronos

        public bool Insert(Customers customers)
        {
            return _customerRepository.Insert(customers);
        }

        public bool Update(Customers customers)
        {
            return _customerRepository.Update(customers);
        }


        public IEnumerable<Customers> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customers Get(string customerId)
        {
            return _customerRepository.Get(customerId);
        }

        public bool Delete(string customerId)
        {
            return _customerRepository.Delete(customerId);
        }

        #endregion


        #region Métodos Asincronos
        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customers> GetAsync(string customerId)
        {
            return await _customerRepository.GetAsync(customerId);
        }


        public async Task<bool> InsertAsync(Customers customers)
        {
            return await _customerRepository.InsertAsync(customers);
        }

        public async Task<bool> UpdateAsync(Customers customers)
        {
            return await _customerRepository.UpdateAsync(customers);
        }


        #endregion



    }
}
