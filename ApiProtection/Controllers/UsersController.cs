using ApiProtection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProtection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)] // Duration = seconds
        public IEnumerable<string> Get()
        {
            return new string[] { $"{Random.Shared.Next(1,101)}" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        //[ResponseCache(Duration = 60*60*24, Location = ResponseCacheLocation.Any, NoStore = false)] // Cache is per id, each id will have a random number per day
        public string Get(int id)
        {
            return $"Random Number: {Random.Shared.Next(1, 101)} for id {id}";
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (!ModelState.IsValid) return BadRequest(user);

            return Ok(user);
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
