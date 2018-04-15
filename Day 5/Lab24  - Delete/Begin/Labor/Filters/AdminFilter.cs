using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Labor.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Session.GetString("SessionKeyName") == "Admin"))
                filterContext.Result = new ContentResult
                {
                    Content = "Unauthorized to access specified resource."
                };
        }
    }
}