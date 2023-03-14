using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Authentication;

public class AuthenticationFiltering : IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    if (!context.HttpContext.User.Identities.Any(x => x.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme))
    {
      context.Result = new UnauthorizedObjectResult("You are not authorized");
      return;
    }
  }
}