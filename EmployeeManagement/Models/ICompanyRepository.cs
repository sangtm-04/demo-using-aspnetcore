using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface ICompanyRepository
    {
        Company GetCompany(int id);
        IEnumerable<Company> GetCompanies();
        Company Insert(Company company);
        Company Update(Company companyChange);
        Company Delete(int id);
    }
}
