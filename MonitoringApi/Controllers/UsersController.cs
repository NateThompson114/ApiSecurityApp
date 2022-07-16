using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            throw new Exception("Something bad happened here...");
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //if (id is < 0 or > 100)
            //{
            //    // By putting not using string interpolation, this log will be a structured error, with a parameter of Identifier with a value, you can then search it
            //    _logger.LogWarning("The given id of {Identifier} was invalid.", id);

            //    return BadRequest("The index was out of range.");
            //}
                
            //_logger.LogInformation(@"The api\users\{id} was called", id);
            //return Ok($"Value{id}");
            try
            {
                if (id is < 0 or > 100)
                    throw new ArgumentOutOfRangeException(nameof(id));
                
                _logger.LogInformation(@"The api\users\{id} was called", id);
                return Ok($"Value{id}");
            }
            catch (Exception ex)
            {
                // By putting not using string interpolation, this log will be a structured error, with a parameter of Identifier with a value, you can then search it
                _logger.LogError(ex, "The given id of {Identifier} was invalid.", id);
                return BadRequest("The index was out of range.");

            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
