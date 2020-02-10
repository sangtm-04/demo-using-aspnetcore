using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<RoleCompany> RoleCompany { get; set; }

        public DbSet<UserRoleCompany> UserRoleCompany { get; set; }

        public DbSet<UserCompany> UserCompany { get; set; }

        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<RoleCompany>().HasKey(u => new { u.CompanyId, u.RoleId });
            modelBuilder.Entity<UserCompany>().HasKey(u => new { u.CompanyId, u.UserId });
            modelBuilder.Entity<UserRoleCompany>().HasKey(u => new { u.CompanyId, u.RoleId, u.UserId });
        }
    }
}
