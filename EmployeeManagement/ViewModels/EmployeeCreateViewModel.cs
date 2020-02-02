using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên không được dài quá 255 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vị trí làm việc")]
        public Dept? Department { get; set; }

        public IFormFile Photo { get; set; }
    }
}
