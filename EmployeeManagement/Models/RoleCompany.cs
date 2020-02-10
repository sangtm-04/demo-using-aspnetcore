using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class RoleCompany
    {
        [Required, StringLength(36)]
        public string CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; }
    }
}
