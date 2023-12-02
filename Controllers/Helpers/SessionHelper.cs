public static class SessionHelper
{
    public static void SetUserSession(HttpContext httpContext, int userId, string firstName, string lastName, string email, string userType)
    {
        httpContext.Session.SetInt32("SessionUserId", userId);
        httpContext.Session.SetString("SessionUserFirstName", firstName);
        httpContext.Session.SetString("SessionUserLastName", lastName);
        httpContext.Session.SetString("SessionUserEmail", email);
        httpContext.Session.SetString("SessionUserType", userType);
    }

    public static void ClearUserSession(HttpContext httpContext)
    {
        httpContext.Session.Clear();
    }
}
