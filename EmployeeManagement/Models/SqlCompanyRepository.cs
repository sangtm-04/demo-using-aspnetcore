using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlCompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public SqlCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public Company Delete(int id)
        {
            Company company = _context.Company.Find(id);
            if (company != null)
            {
                _context.Company.Remove(company);
                _context.SaveChanges();
            }
            return company;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _context.Company;
        }

        public Company GetCompany(int id)
        {
            return _context.Company.Find(id);
        }

        public Company Insert(Company company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();
            return company;
        }

        public Company Update(Company companyChange)
        {
            var company = _context.Company.Attach(companyChange);
            company.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return companyChange;
        }
    }
}
