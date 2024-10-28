using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RequiresUserLogin : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // check if Admin is logged in
        if (context.HttpContext.Session.GetString("UserSession") != "LoggedIn")
        {
            // If not, return an Unauthorized response
            context.Result = new UnauthorizedResult();
        }
    }
}