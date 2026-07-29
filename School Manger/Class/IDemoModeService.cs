using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//for useage 
// put this attribute on action to disable it happy use KingStar
//        [DisableInDemoAttribute]

namespace School_Manger.Class
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DisableInDemoAttribute : ActionFilterAttribute
    {
        private static readonly bool IsDemoMode = true;

        public DisableInDemoAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsDemoMode)
            {
                base.OnActionExecuting(context);
                return;
            }
            if (context.HttpContext.Request.Path.ToString().Contains("/api/") ||
                context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = $"feature is disabled in demo version"
                })
                {
                    StatusCode = 403
                };
            }
            else
            {
                // For MVC requests
                context.Result = new RedirectResult("/Home/BadRequst");
            }
            return;
        }
    }
}
