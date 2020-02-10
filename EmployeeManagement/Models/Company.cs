using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Company
    {
        public Company()
        {

        }

        public Company(string companyId, string companyCode, string companyName)
        {
            CompanyId = companyId;
            CompanyCode = companyCode;
            CompanyName = companyName;
        }

        [StringLength(36)]
        public string CompanyId { get; set; }

        [MaxLength(25)]
        public string CompanyCode { get; set; }

        [MaxLength(255)]
        public string CompanyName { get; set; }
    }
}
