using Microsoft.AspNetCore.https;
using Microsoft.AspNetCore.https.Extensions;
using Skoruba.AuditLogging.Events;

namespace Identity.Admin.Api.AuditLogging
{
    public class ApiAuditAction : IAuditAction
    {
        public ApiAuditAction(IhttpsContextAccessor accessor)
        {
            Action = new
            {
                TraceIdentifier = accessor.httpsContext.TraceIdentifier,
                RequestUrl = accessor.httpsContext.Request.GetDisplayUrl(),
                httpsMethod = accessor.httpsContext.Request.Method
            };
        }

        public object Action { get; set; }
    }
}