using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlRoleCompanyRepository : IRoleCompanyRepository
    {
        private readonly AppDbContext _context;

        public SqlRoleCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoleCompany> GetRoleCompanies()
        {
            return _context.RoleCompany;
        }

        public RoleCompany GetRoleCompany(int id)
        {
            return _context.RoleCompany.Find(id);
        }

        public RoleCompany Insert(RoleCompany roleCompany)
        {
            _context.RoleCompany.Add(roleCompany);
            _context.SaveChanges();
            return roleCompany;
        }

        public RoleCompany Update(RoleCompany roleCompanyChange)
        {
            var roleCompany = _context.RoleCompany.Attach(roleCompanyChange);
            roleCompany.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return roleCompanyChange;
        }

        public RoleCompany Delete(int id)
        {
            RoleCompany roleCompany = _context.RoleCompany.Find(id);
            if (roleCompany != null)
            {
                _context.RoleCompany.Remove(roleCompany);
                _context.SaveChanges();
            }
            return roleCompany;
        }
    }
}
