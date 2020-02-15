using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Requirements
{
    public class RoleCompanyAuthorizationRequirement : AuthorizationHandler<RoleCompanyAuthorizationRequirement>, IAuthorizationRequirement
    {
        public IEnumerable<string> AllowedRoles { get; }

        public RoleCompanyAuthorizationRequirement(IEnumerable<string> allowedRoles)
        {
            if (allowedRoles == null)
            {
                throw new ArgumentNullException(nameof(allowedRoles));
            }

            if (allowedRoles.Count() == 0)
            {
                //throw new InvalidOperationException(Resources.Exception_RoleRequirementEmpty);
                throw new Exception("Loi");
            }
            AllowedRoles = allowedRoles;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleCompanyAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                bool found = false;
                if (requirement.AllowedRoles == null || !requirement.AllowedRoles.Any())
                {
                    // Review: What do we want to do here?  No roles requested is auto success?
                }
                else
                {
                    found = requirement.AllowedRoles.Any(r => context.User.IsInRole(r));
                    // TODO: Xử lý điều kiện tìm bất kỳ user nào trong bảng UserRoleCompany có User - Role - Company thỏa mãn điều kiện
                }
                if (found)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
