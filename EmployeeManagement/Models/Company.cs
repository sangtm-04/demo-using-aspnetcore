using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Company
    {
        [StringLength(36)]
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
