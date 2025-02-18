using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SSVH_Consultation_Poratl.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RoleFilter : Attribute, IActionFilter
    {
        private readonly List<int> _requiredRoles;

        public RoleFilter(params int[] requiredRoles)
        {
            _requiredRoles = new List<int>(requiredRoles);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var role = Convert.ToString(context.HttpContext.Session.GetInt32("RoleId"));
            if (string.IsNullOrEmpty(role) ||!int.TryParse(role, out var userRole) || !_requiredRoles.Contains(userRole))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Error", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
