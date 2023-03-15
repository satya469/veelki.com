using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Veelki.Core.ServiceHelper
{
    public class CustomActionFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(Convert.ToString(context.HttpContext.Session.GetString("JWToken"))))
            {
                context.Result = new RedirectResult("/User/Login");
            }
        }
    }
}

