using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IRoleCompanyRepository
    {
        RoleCompany GetRoleCompany(int id);
        IEnumerable<RoleCompany> GetRoleCompanies();
        RoleCompany Insert(RoleCompany roleCompany);
        RoleCompany Update(RoleCompany roleCompanyChange);
        RoleCompany Delete(int id);
    }
}
