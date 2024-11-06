using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RequiresAdminLogin : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // check if Admin is logged in
        if (context.HttpContext.Session.GetString("AdminSession") != "LoggedIn")
        {
            // If not, return an Unauthorized response
            context.Result = new UnauthorizedResult();
        }
    }
}
