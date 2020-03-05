using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models_Resources;

namespace ProcessStat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessStatsController : ControllerBase
    {
        private ILogger _logger = null;

        public ProcessStatsController(ILogger<ProcessStatsController> logger)
        {
            _logger = logger;
        }

        public ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        // GET: api/ProcessStats
        [HttpGet]
        public ActionResult<ProcessStats> GetProcessStats()
        {
            try
            {
                ProcessStatsResourcer resourcer = new ProcessStatsResourcer();
                ProcessStats processStats = resourcer.Retrieve(Logger);

                return Ok(processStats);
            }
            catch (ResourceFindException exception)
            {
                Logger.LogError($"App Level Exception: {exception.Message}", exception);

                return StatusCode(ApiServiceFailure.ServiceFailure, exception.Message);
            }
            catch (Exception exception)
            {
                Logger.LogError($"Endpoint Exception: {exception.Message}", exception);

                return StatusCode(ApiServiceFailure.ServiceFailure);
            }
        }
    }
}
