using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext context;
        private readonly ILogger _logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, AppDbContext context, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            this.context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index(string companyCode)
        {
            if (companyCode == null)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                return RedirectToAction("/Home/ListEmployees", companyCode);
            }
        }

        public IActionResult ListEmployees(string companyCode)
        {
            var company = GetCompanyByCompanyCode(companyCode);
            var employees = _employeeRepository.GetEmployees(company.CompanyId);
            return View(employees);
        }

        public Company GetCompanyByCompanyCode(string companyCode)
        {
            return context.Company.FirstOrDefault(company => company.CompanyCode == companyCode);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id, string companyCode)
        {
            //throw new Exception("Error in Details View");

            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

            var company = GetCompanyByCompanyCode(companyCode);
            Employee employee = _employeeRepository.GetEmployee(id.Value, company.CompanyId);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"

            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model, string companyCode)
        {
            if (ModelState.IsValid)
            {
                var company = GetCompanyByCompanyCode(companyCode);
                string uniqueFileName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName,
                    CompanyId = company.CompanyId
                };
                _employeeRepository.Insert(newEmployee);
                return RedirectToAction("details", "home", new { id = newEmployee.Id, companyCode });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id, string companyCode)
        {
            var company = GetCompanyByCompanyCode(companyCode);
            Employee employee = _employeeRepository.GetEmployee(id, company.CompanyId);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model, string companyCode)
        {
            if (ModelState.IsValid)
            {
                var company = GetCompanyByCompanyCode(companyCode);
                Employee employee = _employeeRepository.GetEmployee(model.Id, company.CompanyId);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
