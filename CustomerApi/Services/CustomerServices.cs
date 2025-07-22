using CustomerApi.IServices;
using CustomerApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CustomerService : ICustomer
{
    private readonly IDbServices _db;

    public CustomerService(IDbServices db)
    {
        _db = db;
    }

    
    public Task<Customer> GetCustomerById(int id)
    {
        return _db.GetAsync<Customer>("sp_get_employee_by_id", new { emp_id = id });
    }

    
    public Task<List<Customer>> GetAllCustomers()
    {
        return _db.GetAll<Customer>("sp_get_all_employees", null);
    }

    
    public Task<int> AddCustomer(Customer customer)
    {
        return _db.EditData("sp_insert_employee", new { emp_name = customer.Name, customer_age = customer.Age });
    }

    
    public Task<int> UpdateCustomer(Customer customer)
    {
        return _db.EditData("sp_update_employee", new { emp_id = customer.Id, emp_name = customer.Name, emp_age = customer.Age });
    }

    
    public Task<int> DeleteCustomer(int id)
    {
        return _db.EditData("sp_delete_employee", new { emp_id = id });
    }
}
