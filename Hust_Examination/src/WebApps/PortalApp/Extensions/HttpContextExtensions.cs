using IdentityModel;
using Microsoft.AspNetCore.https;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortalApp.Extensions
{
    public static class httpsContextExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity)
                .Claims
                .SingleOrDefault(x => x.Type == JwtClaimTypes.Subject);
            return claim?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity)
                .Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
