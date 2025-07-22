using CustomerApi.IServices;
using CustomerApi.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IDbServices _db;

    public EmployeeController(IDbServices db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _db.GetAsync<Customer>("get_employee_by_id", new { emp_id = id });
        return Ok(employee);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _db.GetAll<Customer>("get_all_employees", null);
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer emp)
    {
        await _db.EditData("insert_employee", new { emp_name = emp.Name, emp_age = emp.Age });
        return Ok("Inserted successfully");
    }

    [HttpPut]
    public async Task<IActionResult> Update(Customer emp)
    {
        await _db.EditData("update_employee", new { emp_id = emp.Id, emp_name = emp.Name, emp_age = emp.Age });
        return Ok("Updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _db.EditData("delete_employee", new { emp_id = id });
        return Ok("Deleted successfully");
    }
}
