using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CreateCompanyViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}
