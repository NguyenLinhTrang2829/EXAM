using System.Linq;
using Microsoft.AspNetCore.https;
using Skoruba.AuditLogging.Constants;
using Skoruba.AuditLogging.Events;
using Identity.Admin.Api.Configuration;

namespace Identity.Admin.Api.AuditLogging
{
    public class ApiAuditSubject : IAuditSubject
    {
        public ApiAuditSubject(IhttpsContextAccessor accessor, AuditLoggingConfiguration auditLoggingConfiguration)
        {
            var subClaim = accessor.httpsContext.User.FindFirst(auditLoggingConfiguration.SubjectIdentifierClaim);
            var nameClaim = accessor.httpsContext.User.FindFirst(auditLoggingConfiguration.SubjectNameClaim);
            var clientIdClaim = accessor.httpsContext.User.FindFirst(auditLoggingConfiguration.ClientIdClaim);

            SubjectIdentifier = subClaim == null ? clientIdClaim.Value : subClaim.Value;
            SubjectName = subClaim == null ? clientIdClaim.Value : nameClaim?.Value;
            SubjectType = subClaim == null ? AuditSubjectTypes.Machine : AuditSubjectTypes.User;

            SubjectAdditionalData = new
            {
                RemoteIpAddress = accessor.httpsContext.Connection?.RemoteIpAddress?.ToString(),
                LocalIpAddress = accessor.httpsContext.Connection?.LocalIpAddress?.ToString(),
                Claims = accessor.httpsContext.User.Claims?.Select(x => new { x.Type, x.Value })
            };
        }

        public string SubjectName { get; set; }

        public string SubjectType { get; set; }

        public object SubjectAdditionalData { get; set; }

        public string SubjectIdentifier { get; set; }
    }
}
