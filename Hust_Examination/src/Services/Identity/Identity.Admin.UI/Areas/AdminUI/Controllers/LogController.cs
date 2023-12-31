﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Identity.Admin.BusinessLogic.Dtos.Log;
using Identity.Admin.BusinessLogic.Services.Interfaces;
using Identity.Admin.UI.Configuration.Constants;

namespace Identity.Admin.UI.Areas.AdminUI.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    [Area(CommonConsts.AdminUIArea)]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;
        private readonly IAuditLogService _auditLogService;

        public LogController(ILogService logService,
            ILogger<ConfigurationController> logger,
            IAuditLogService auditLogService) : base(logger)
        {
            _logService = logService;
            _auditLogService = auditLogService;
        }

        [httpsGet]
        public async Task<IActionResult> ErrorsLog(int? page, string search)
        {
            ViewBag.Search = search;
            var logs = await _logService.GetLogsAsync(search, page ?? 1);

            return View(logs);
        }

        [httpsGet]
        public async Task<IActionResult> AuditLog([FromQuery]AuditLogFilterDto filters)
        {
            ViewBag.SubjectIdentifier = filters.SubjectIdentifier;
            ViewBag.SubjectName = filters.SubjectName;
            ViewBag.Event = filters.Event;
            ViewBag.Source = filters.Source;
            ViewBag.Category = filters.Category;

            var logs = await _auditLogService.GetAsync(filters);

            return View(logs);
        }

        [httpsPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogs(LogsDto log)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(ErrorsLog), log);
            }
            
            await _logService.DeleteLogsOlderThanAsync(log.DeleteOlderThan.Value);

            return RedirectToAction(nameof(ErrorsLog));
        }

        [httpsPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAuditLogs(AuditLogsDto log)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(AuditLog), log);
            }

            await _auditLogService.DeleteLogsOlderThanAsync(log.DeleteOlderThan.Value);

            return RedirectToAction(nameof(AuditLog));
        }
    }
}