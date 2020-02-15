using EmployeeManagement.Services.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Handlers
{
    public class RoleCompanyAuthorizationHandler : AuthorizationHandler<RoleCompanyAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleCompanyAuthorizationRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
