using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.MiddlewareFilters
{
    public class ShoppingCartFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine();
            if (!context.HttpContext.Session.Keys.Contains("Cart"))
            {
                context.HttpContext.Session.SetString("Cart", "empty");
            }

            base.OnActionExecuting(context);
        }
    }
}

