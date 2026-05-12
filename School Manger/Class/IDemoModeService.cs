using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School_Manger.Class
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DisableInDemoAttribute : ActionFilterAttribute
    {
        private readonly string[] _disabledFeatures;

        // 👇 HARDCODE demo mode here - change this one line to enable/disable
        private static readonly bool IsDemoMode = true;  // Set to false for full version

        // Hardcoded disabled features
        private static readonly List<string> DisabledFeatures = new()
        {
            "Delete", "Edit", "Create", "Export", "Import", "Settings"
        };

        public DisableInDemoAttribute(params string[] features)
        {
            _disabledFeatures = features.Length > 0 ? features : DisabledFeatures.ToArray();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // If not in demo mode, allow everything
            if (!IsDemoMode)
            {
                base.OnActionExecuting(context);
                return;
            }
            // For API requests
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
                context.Result = new RedirectResult("/Home/DemoDisabled");
            }
            return;
        }
    }
}
