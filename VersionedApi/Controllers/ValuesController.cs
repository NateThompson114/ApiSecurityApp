using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// This can get really messy and isn't entirely recommended as it can get bloated really fast

namespace VersionedApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "v1 value1", "v1 value2" };
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> GetV2()
        {
            return new string[] { "v2 value1", "v2 value2" };
        }

        // GET: api/<UsersController>
        [HttpGet("extra")]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> NewExtraGet()
        {
            return new string[] { "New Extra v2 value1", "New Extra v2 value2" };
        }
    }
}
