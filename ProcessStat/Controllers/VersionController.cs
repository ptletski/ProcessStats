using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ProcessStat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        // GET api/version
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "ProcessStats", "1.0.1" };
        }
    }
}
