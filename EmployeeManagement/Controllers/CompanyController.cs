using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IRoleCompanyRepository roleCompanyRepository;
        private readonly IUserRoleCompanyRepository userRoleCompanyRepository;
        private readonly IUserCompanyRepository userCompanyRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public CompanyController(ICompanyRepository companyRepository, IRoleCompanyRepository roleCompanyRepository,
            IUserRoleCompanyRepository userRoleCompanyRepository, IUserCompanyRepository userCompanyRepository, RoleManager<IdentityRole> roleManager)
        {
            this.companyRepository = companyRepository;
            this.roleCompanyRepository = roleCompanyRepository;
            this.userRoleCompanyRepository = userRoleCompanyRepository;
            this.userCompanyRepository = userCompanyRepository;
            this.roleManager = roleManager;
        }

        [Authorize(Policy = "AtLeast21")]
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var companyId = Guid.NewGuid().ToString();
                string shortCode = GenerateShortcode();
                var newCompany = new Company(companyId, shortCode, model.CompanyName);
                companyRepository.Insert(newCompany);
                InsertUserCompany(companyId, model.UserId);
                InsertRoleCompany(companyId);
                InsertUserRoleCompany(companyId, model.UserId);
                return RedirectToAction("/home/listusers", shortCode);
            }
            return View();
        }

        public string GenerateShortcode()
        {
            string shortCode = "";
            int landmark = 1575000000;
            Random rd = new Random();

            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            int timeLMark = unixTimestamp - landmark;
            long base10 = rd.Next(1, timeLMark) + rd.Next(1, 1000) + rd.Next(1, 100) + rd.Next(1, 10);
            ulong ulongBase10 = Convert.ToUInt64(base10 * 1000001);
            shortCode = ToBase62(ulongBase10);
            return shortCode;
        }

        public string ToBase62(ulong number)
        {
            var alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var n = number;
            ulong basis = 62;
            var ret = "";
            while (n > 0)
            {
                ulong temp = n % basis;
                ret = alphabet[(int)temp] + ret;
                n = (n / basis);

            }
            return ret;
        }

        public void InsertRoleCompany(string companyId)
        {
            foreach (var role in roleManager.Roles.ToList())
            {
                var roleCompany = new RoleCompany
                {
                    RoleId = role.Id,
                    CompanyId = companyId
                };

                roleCompanyRepository.Insert(roleCompany);
            }
        }

        public void InsertUserCompany(string companyId, string userId)
        {
            var userCompany = new UserCompany
            {
                UserId = userId,
                CompanyId = companyId
            };
            userCompanyRepository.Insert(userCompany);
        }

        public void InsertUserRoleCompany(string companyId, string userId)
        {
            var userRoleCompany = new UserRoleCompany
            {
                UserId = userId,
                RoleId = roleManager.Roles.FirstOrDefault(role => role.Name == "Admin").Id,
                CompanyId = companyId
            };
            userRoleCompanyRepository.Insert(userRoleCompany);
        }
    }
}