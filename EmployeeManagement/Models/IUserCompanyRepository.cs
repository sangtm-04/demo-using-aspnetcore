using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IUserCompanyRepository
    {
        UserCompany GetUserCompany(string userId, string companyId);
        IEnumerable<UserCompany> GetUserCompanies(string companyId);
        UserCompany Insert(UserCompany userCompany);
        UserCompany Update(UserCompany userCompanyChange);
        UserCompany Delete(int id);
    }
}
