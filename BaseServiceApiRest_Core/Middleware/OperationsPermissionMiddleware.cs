
using BaseServiceApiRest_Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace BaseServiceApiRest_Core.Middleware;

public class OperationsPermissionMiddleware
{
    private readonly RequestDelegate _next;

    public OperationsPermissionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Items.Add("isAnonymous", false);

        var endpointFeature = context.Features.Get<IEndpointFeature>();
        if (endpointFeature is not null)
        {
            var endpoint = endpointFeature.Endpoint;
            if (endpoint is not null)
            {
                var anonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>();
                if (anonymous is not null)
                {
                    context.Items["isAnonymous"] = true;
                    await _next(context);
                    return;
                }
            }
        }

        if (!HasPermission(context))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }

    private static bool HasPermission(HttpContext context)
    {
        var role = context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

        bool isAdmin = role == UserTypeEnum.Admin.ToString();
        bool isManager = role == UserTypeEnum.Manager.ToString();
        bool isLeader = role == UserTypeEnum.Leader.ToString();

        return isAdmin || isLeader || isManager;
    }
}