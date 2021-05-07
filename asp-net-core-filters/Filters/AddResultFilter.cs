using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_net_core_filters.Filters
{
    public class AddResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add(
                "AppID",
                "Geeks App header was added by result filter.");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
         
        }
    }
}
