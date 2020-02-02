using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        Employee Insert(Employee employee);
        Employee Update(Employee employeeChange);
        Employee Delete(int id);
    }
}
