using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    protected void SetUserSession(int userId, string firstName, string lastName, string email, string userType)
    {
        SessionHelper.SetUserSession(HttpContext, userId, firstName, lastName, email, userType);
    }

    protected void ClearUserSession()
    {
        SessionHelper.ClearUserSession(HttpContext);
    }

    protected void SetViewDataFromSession()
    {
        ViewData["LoggedUserId"] = HttpContext.Session.GetInt32("SessionUserId");
        ViewData["LoggedUserFirstName"] = HttpContext.Session.GetString("SessionUserFirstName") ?? string.Empty;
        ViewData["LoggedUserLastName"] = HttpContext.Session.GetString("SessionUserLastName") ?? string.Empty;
        ViewData["LoggedUserEmail"] = HttpContext.Session.GetString("SessionUserEmail") ?? string.Empty;
        ViewData["LoggedUserType"] = HttpContext.Session.GetString("SessionUserType") ?? string.Empty;
    }

    protected bool IsUserLoggedIn()
    {
        int? userId = HttpContext.Session.GetInt32("SessionUserId");
        return userId.HasValue && !string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserEmail"));
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerName = context.RouteData.Values["controller"]?.ToString();

        var isLoginPage = string.Equals(controllerName, "Home", StringComparison.OrdinalIgnoreCase); 
           
        if (!isLoginPage && !IsUserLoggedIn())
        {
            context.Result = RedirectToAction("Login", "Home");
            return;
        }

        SetViewDataFromSession();

        context.HttpContext.Response.Headers["Cache-Control"] = "no-store, no-cache";
        context.HttpContext.Response.Headers["Pragma"] = "no-cache";
        context.HttpContext.Response.Headers["Expires"] = "-1";

        base.OnActionExecuting(context);
    }
}
