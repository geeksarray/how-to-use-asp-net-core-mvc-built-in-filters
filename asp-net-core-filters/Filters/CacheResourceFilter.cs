using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace asp_net_core_filters.Filters
{
    public class CacheResourceFilter : IResourceFilter 
    {
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;

        public CacheResourceFilter()
        {   
            this._memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();

            string contentResult = string.Empty;

            contentResult = _memoryCache.Get<string>(_cacheKey);

            if(!string.IsNullOrEmpty(contentResult))
            {
                context.Result = new ContentResult()
                { Content = contentResult };
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!string.IsNullOrEmpty(_cacheKey))
            {
                var result = context.Result as ContentResult;
                if (result != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(_cacheKey, result.Content,
                                cacheEntryOptions);
                }
            }
        }
    }
}
