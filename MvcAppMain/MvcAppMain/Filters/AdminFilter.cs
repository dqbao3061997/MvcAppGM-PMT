using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppMain.Filters
{
    public class AdminFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (filterContext.HttpContext.Session["Roles"].Equals("user")) {
                filterContext.Result = new ContentResult { Content = "Unathorized" };
            }
        }
    }
}