using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Sang", Email="sang@gmail.com", Department = "IT"},
                new Employee() {Id = 2, Name = "Hung", Email="hung@gmail.com", Department = "HR"},
                new Employee() {Id = 3, Name = "Giang", Email="giang@gmail.com", Department = "IT"}
            };
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }
    }
}
