using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            context.Result = new ForbidResult();
        }
        
        var roleString = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == $"project_{projectId}")?.Value;
        if (string.IsNullOrEmpty(roleString))
        {
            context.Result = new ForbidResult();
        }
        else
        {
            var role = Enum.Parse<ProjectRole>(roleString);
            if (!_grantedRoles.Contains(role))
            {
                context.Result = new ForbidResult();
            }
        }
        
    }
}