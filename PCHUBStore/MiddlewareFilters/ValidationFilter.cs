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


        private readonly ILaptopServices service;

        public ValidationFilter(ILaptopServices service)
        {
            this.service = service;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var canContinue = true;
            if (context.ModelState.IsValid)
            {
                if (!context.HttpContext.Request.QueryString.HasValue)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    var laptopFilterCategory = this.service.GetFilters("Laptops").GetAwaiter().GetResult();

                    var urlParameters = context.HttpContext.Request.Query.ToList();

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
                            if(decimal.TryParse(value[0], out decimal num) && num >= 0 && num <= 30000)
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

                        if (!canContinue)
                        {
                            break;
                        }

                        foreach (var valueParam in value)
                        {
                            if ((laptopFilterCategory.Any(x => x.Filters.Any(z => z.Name.ToLower() == key.ToLower() && z.Value.ToLower().Contains(valueParam.ToLower())))
                                || (valueParam == "All" && laptopFilterCategory.Any(x => x.Filters.Any(z => z.Name == key)))))
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
                redirectTargetDictionaryRedirect.Add("action", "Laptops");
                redirectTargetDictionaryRedirect.Add("controller", "Products");

                var urlParametersRedirect = new RedirectToRouteResult(redirectTargetDictionaryRedirect);
                context.Result = urlParametersRedirect;
                base.OnActionExecuting(context);
            }


            base.OnActionExecuting(context);

        }
    }
}
