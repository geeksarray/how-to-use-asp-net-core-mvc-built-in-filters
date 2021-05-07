using asp_net_core_filters.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlTypes;

namespace asp_net_core_filters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult ThrowSomeException()
        {
            throw new SqlNullValueException();
        }

        [ServiceFilter(typeof(AuthorizeIPAddress))]
        [ServiceFilter(typeof(AddResultFilter))]
        public IActionResult Index()
        {
            return View();
        }
        
        [ServiceFilter(typeof(CacheResourceFilter))]
        public IActionResult Message()
        {
            return Content("This content was generated at " + DateTime.Now);
        }
        
        [ServiceFilter(typeof(TimeTaken))]
        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]        
        public IActionResult Error()
        {
            return View();
        }
    }
}
