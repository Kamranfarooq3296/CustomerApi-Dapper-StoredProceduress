using CustomerApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.IServices
{
    public interface ICustomer
    {
        Task<Customer> GetCustomerById(int id);
        Task<List<Customer>> GetAllCustomers();
        Task<int> AddCustomer(Customer customer);
        Task<int> UpdateCustomer(Customer customer);
        Task<int> DeleteCustomer(int id);
    }
}
