using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId;
        private IPermissionService _permissionService;

        public PermissionCheckerAttribute(int permissionId, IPermissionService permissionService)
        {
            _permissionId = permissionId;
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                string userName = context.HttpContext.User.Identity.Name;

                if (!_permissionService.Checkpermission(_permissionId, userName))
                {
                    context.Result = new RedirectResult($"/Login?{context.HttpContext.Request.Path}");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
