using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class HealthController1 : ControllerBase
    {
        [HttpGet]
        [Route("HealthCheck")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Db = Good", "Support Apis = Good", "Resources = Good" };
        }
    }
}
