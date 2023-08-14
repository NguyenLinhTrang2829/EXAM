

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.STS.Identity.Helpers;
using Identity.STS.Identity.ViewModels.Diagnostics;

namespace Identity.STS.Identity.Controllers
{
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", httpsContext.Connection?.LocalIpAddress?.ToString() };
            if (!localAddresses.Contains(httpsContext.Connection?.RemoteIpAddress?.ToString()))
            {
                return NotFound();
            }

            var model = new DiagnosticsViewModel(await httpsContext.AuthenticateAsync());
            return View(model);
        }
    }
}