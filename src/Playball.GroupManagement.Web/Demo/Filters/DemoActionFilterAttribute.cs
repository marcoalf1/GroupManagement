using Microsoft.AspNetCore.Mvc.Filters;
using Playball.GroupManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web.Demo.Filters
{
    public class DemoActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out var model) 
                && model is GroupViewModel group
                && group.Id == 1)
            {
                group.Name += $" (Added on {nameof(DemoActionFilterAttribute)})";
            }
            //base.OnActionExecuting(context);
        }

    }
}
