using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Contracts.Constants;
using Teams.Domain.Enums;

namespace Shared.Api.Attributes;

public class ProjectAuthAttribute: Attribute, IAuthorizationFilter
{
    private List<ProjectRole> _grantedRoles;

    public ProjectAuthAttribute(params ProjectRole[] grantedRoles)
    {
        _grantedRoles = grantedRoles.ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var projectId = context.RouteData.Values["projectId"]?.ToString();
        if (projectId == null)
        {
            throw new ArgumentException("Project Id is not provided");
        }

        var roleIdString = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenConstants.ROLE)?.Value;
        var projectIdString = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenConstants.PROJECT_ID)?.Value;

        if (projectId != projectIdString)
        {
            context.Result = new ForbidResult();
            return;
        }
        
        if (roleIdString == null || !int.TryParse(roleIdString, out int roleId))
        {
            context.Result = new ForbidResult();
            return;
        }

        var role = (ProjectRole)roleId;
        if (!_grantedRoles.Contains(role))
        {
            context.Result = new ForbidResult();
        }
    }
}