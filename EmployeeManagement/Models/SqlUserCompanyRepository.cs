using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlUserCompanyRepository : IUserCompanyRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SqlUserCompanyRepository> _logger;

        public SqlUserCompanyRepository(AppDbContext context, ILogger<SqlUserCompanyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public UserCompany Delete(int id)
        {
            UserCompany userCompany = _context.UserCompany.Find(id);
            if (userCompany != null)
            {
                _context.UserCompany.Remove(userCompany);
                _context.SaveChanges();
            }
            return userCompany;
        }

        public IEnumerable<UserCompany> GetUserCompanies(string companyId)
        {
            return _context.UserCompany.Where(userCompany => userCompany.CompanyId == companyId);
        }

        public UserCompany GetUserCompany(string userId, string companyId)
        {
            return _context.UserCompany.FirstOrDefault(userCompany => userCompany.CompanyId == companyId && userCompany.UserId == userId);
        }

        public UserCompany Insert(UserCompany userCompany)
        {
            _context.UserCompany.Add(userCompany);
            _context.SaveChanges();
            return userCompany;
        }

        public UserCompany Update(UserCompany userCompanyChange)
        {
            throw new NotImplementedException();
        }
    }
}
