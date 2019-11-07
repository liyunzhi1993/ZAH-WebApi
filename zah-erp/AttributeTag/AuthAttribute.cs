using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zah_core.Service;
using zah_core.Service.Impl;
using zah_db.Entity;

namespace zah_erp.AttributeTag
{
    public class AuthAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var erpAccountService = (ErpAccountServiceImpl)context.HttpContext.RequestServices.GetService(typeof(IErpAccountService));

            //if (erpAccountService.Authenticate.IsLogin)
            //{
            //    base.OnActionExecuting(context);
            //}
            //else
            //{
            //    string url = this.GetRedirectUrl(context.HttpContext.Request);

            //    context.Result = new RedirectToActionResult("Login", "Account", new { redirect = url });
            //}
        }

        private string GetRedirectUrl(HttpRequest request)
        {
            var builder = new UriBuilder()
            {
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            return builder.Uri.PathAndQuery;
        }
    }
}
