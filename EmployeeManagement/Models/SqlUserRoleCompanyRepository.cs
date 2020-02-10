using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlUserRoleCompanyRepository : IUserRoleCompanyRepository
    {
        private readonly AppDbContext _context;

        public SqlUserRoleCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserRoleCompany Delete(int id)
        {
            UserRoleCompany userRoleCompany = _context.UserRoleCompany.Find(id);
            if (userRoleCompany != null)
            {
                _context.UserRoleCompany.Remove(userRoleCompany);
                _context.SaveChanges();
            }
            return userRoleCompany;
        }

        public IEnumerable<UserRoleCompany> GetUserRoleCompanies()
        {
            return _context.UserRoleCompany;
        }

        public UserRoleCompany GetUserRoleCompany(int id)
        {
            return _context.UserRoleCompany.Find(id);
        }

        public UserRoleCompany Insert(UserRoleCompany userRoleCompany)
        {
            _context.UserRoleCompany.Add(userRoleCompany);
            _context.SaveChanges();
            return userRoleCompany;
        }

        public UserRoleCompany Update(UserRoleCompany userRoleCompanyChange)
        {
            var userRoleCompany = _context.UserRoleCompany.Attach(userRoleCompanyChange);
            userRoleCompany.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return userRoleCompanyChange;
        }
    }
}
