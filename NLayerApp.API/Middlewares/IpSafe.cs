using Microsoft.Extensions.Options;
using NLayerApp.Core.Models;
using System.Net;

namespace NLayerApp.API.Middlewares
{
    public class IpSafe
    {
        private readonly RequestDelegate _next;
        private readonly IpList _ipList;

        public IpSafe(RequestDelegate next, IOptions<IpList> ipList)
        {
            _next = next;
            _ipList = ipList.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestIpAddress = context.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(requestIpAddress)).Any();
            if (!isWhiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
