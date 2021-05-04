using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace asp_net_core_filters.Filters
{
    public class AuthorizeIPAddress : IAuthorizationFilter
    {
        private readonly string _allowedIPAddress;
        public AuthorizeIPAddress(string allowedIPAddress)
        {
            this._allowedIPAddress = allowedIPAddress;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {  
            var requestIp = context.HttpContext.Connection.RemoteIpAddress;

            var ipAddresses = this._allowedIPAddress.Split(';');
            var unauthorizedIp = true;

            if (requestIp.IsIPv4MappedToIPv6)
            {
                requestIp = requestIp.MapToIPv4();
            }

            foreach (var address in ipAddresses)
            {
                var testIp = IPAddress.Parse(address);

                if (testIp.Equals(requestIp))
                {
                    unauthorizedIp = false;
                    break;
                }
            }

            if (unauthorizedIp)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
        }
    }
}
