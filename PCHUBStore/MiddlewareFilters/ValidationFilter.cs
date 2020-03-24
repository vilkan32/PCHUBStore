using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using PCHUBStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.MiddlewareFilters
{
    public class ValidationFilter : ActionFilterAttribute
    {


        private readonly IProductServices service;

        public ValidationFilter(IProductServices service)
        {
            this.service = service;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var canContinue = true;
            if (context.ModelState.IsValid)
            {
                var cat = context.HttpContext.Request.RouteValues.FirstOrDefault(x => x.Key == "category");

                var categ = (string)cat.Value;
                if (!this.service.CategoryExistsAsync(categ).GetAwaiter().GetResult())
                {
                    RouteValueDictionary redirectTargetDictionaryRedirect = new RouteValueDictionary();
                    redirectTargetDictionaryRedirect.Add("action", "Error");
                    redirectTargetDictionaryRedirect.Add("controller", "Home");

                    var urlParametersRedirect = new RedirectToRouteResult(redirectTargetDictionaryRedirect);
                    context.Result = urlParametersRedirect;
                    base.OnActionExecuting(context);
                }
         
                if (!context.HttpContext.Request.QueryString.HasValue)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    var urlParameters = context.HttpContext.Request.Query.ToList();

                    var kvp = context.HttpContext.Request.RouteValues.FirstOrDefault(x => x.Key == "category");

                    var category = (string)kvp.Value;

                    var productFilterCategory = this.service.GetFiltersAsync(category).GetAwaiter().GetResult();
                    if(productFilterCategory.Count == 0)
                    {
                        canContinue = false;
                    }

                    foreach (var param in urlParameters)
                    {
                        var key = param.Key;
                        var value = param.Value.ToArray();
                        if (key == "OrderBy")
                        {

                            if (value[0] == "Default" || value[0] == "PriceDesc" || value[0] == "PriceAsc")
                            {
                                continue;
                            }
                            else
                            {
                                canContinue = false;
                            }
                        }
                        else if (key == "MinPrice")
                        {
                            if (decimal.TryParse(value[0], out decimal num) && num >= 0 && num <= 30000)
                            {
                                continue;
                            }
                            else
                            {
                                canContinue = false;
                            }
                        }
                        else if (key == "MaxPrice")
                        {
                            if (decimal.TryParse(value[0], out decimal num) && num >= 0 && num <= 30000)
                            {
                                continue;
                            }
                            else
                            {
                                canContinue = false;
                            }
                        }
                        else if (key == "Page")
                        {
                            if ((int.TryParse(value[0], out int num) && num > 0 && num < 100))
                            {
                                continue;
                            }
                            else
                            {
                                canContinue = false;
                            }
                        }
                        else if (key == "Category" || key == "category")
                        {
                            continue;
                        }

                        if (!canContinue)
                        {
                            break;
                        }

                        foreach (var valueParam in value)
                        {
                            if ((productFilterCategory.Any(x => x.Filters.Any(z => z.Name.ToLower() == key.ToLower() && z.Value.ToLower().Contains(valueParam.ToLower())))
                                || (valueParam == "All" && productFilterCategory.Any(x => x.Filters.Any(z => z.Name == key)))))
                            {

                            }
                            else
                            {
                                canContinue = false;
                                break;
                            }
                        }



                        if (!canContinue)
                        {
                            break;
                        }

                    }
                }

            }
            else
            {
                canContinue = false;
            }


            if (!canContinue)
            {
                RouteValueDictionary redirectTargetDictionaryRedirect = new RouteValueDictionary();
                redirectTargetDictionaryRedirect.Add("action", "Error");
                redirectTargetDictionaryRedirect.Add("controller", "Home");

                var urlParametersRedirect = new RedirectToRouteResult(redirectTargetDictionaryRedirect);
                context.Result = urlParametersRedirect;
                base.OnActionExecuting(context);
            }


            base.OnActionExecuting(context);

        }
    }
}
