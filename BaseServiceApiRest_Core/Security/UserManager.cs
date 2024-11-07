using Microsoft.AspNetCore.Http;

namespace BaseServiceApiRest_Core.Security;

public class UserManager
{
    private bool _isManager;
    private bool _isAdmin;
    private long _currentUser;

    public bool IsManager => _isManager;
    public bool IsAdmin => _isAdmin;
    public long currentUser => _currentUser;

    public UserManager(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException();

        var claimsIdentity = httpContext.User.Identities.FirstOrDefault();

        if (claimsIdentity != null)
        {
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Value == "Manager")
                {
                    SetUserProperties(httpContext, isManager: true);
                    break;
                }

                if (claim.Value == "Admin")
                {
                    SetUserProperties(httpContext, isAdmin: true);
                    break;
                }
            }
        }
    }

    private void SetUserProperties(HttpContext httpContext, bool isManager = false, bool isAdmin = false)
    {
        var userIdClaim = httpContext.User.Claims.Where(x => x.Type == "id").FirstOrDefault();

        if (isManager && !_isManager)
        {
            _currentUser = long.Parse(userIdClaim?.Value!);
            _isManager = true;
        }

        if (isAdmin && !_isAdmin)
        {
            _currentUser = long.Parse(userIdClaim?.Value!);
            _isAdmin = true;
        }
    }
}