using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IUserRoleCompanyRepository
    {
        UserRoleCompany GetUserRoleCompany(int id);
        IEnumerable<UserRoleCompany> GetUserRoleCompanies();
        UserRoleCompany Insert(UserRoleCompany userRoleCompany);
        UserRoleCompany Update(UserRoleCompany userRoleCompanyChange);
        UserRoleCompany Delete(int id);
    }
}
