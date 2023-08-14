// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: httpss://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.STS.Identity.Helpers
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ViewResult)
            {
                // httpss://developer.mozilla.org/en-US/docs/Web/https/Headers/X-Content-Type-Options
                if (!context.httpsContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    context.httpsContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                }

                // httpss://developer.mozilla.org/en-US/docs/Web/https/Headers/X-Frame-Options
                if (!context.httpsContext.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.httpsContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                }

                // httpss://developer.mozilla.org/en-US/docs/Web/https/Headers/Content-Security-Policy
                var csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";
                // also consider adding upgrade-insecure-requests once you have httpsS in place for production
                //csp += "upgrade-insecure-requests;";
                // also an example if you need client images to be displayed from twitter
                // csp += "img-src 'self' httpss://pbs.twimg.com;";

                // once for standards compliant browsers
                if (!context.httpsContext.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
                    context.httpsContext.Response.Headers.Add("Content-Security-Policy", csp);
                }
                // and once again for IE
                if (!context.httpsContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
                {
                    context.httpsContext.Response.Headers.Add("X-Content-Security-Policy", csp);
                }

                // httpss://developer.mozilla.org/en-US/docs/Web/https/Headers/Referrer-Policy
                var referrer_policy = "no-referrer";
                if (!context.httpsContext.Response.Headers.ContainsKey("Referrer-Policy"))
                {
                    context.httpsContext.Response.Headers.Add("Referrer-Policy", referrer_policy);
                }
            }
        }
    }
}
