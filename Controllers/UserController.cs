using Microsoft.AspNetCore.Mvc;
using MongoDBDemo.Helpers;
using MongoDBDemo.Models;
using MongoDBDemo.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repo = new UserRepository();
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repo.GetUsers();
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            _repo.AddUser(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            _repo.UpdateUser(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteUser(id);
        }
        [HttpPost]
        [Route("validate")]
        public IActionResult Validate(User value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            if (_repo.Validate(value))
            {

                return Ok(new TokenResult()
                {
                    Status = "success",
                    Token = new TokenHelper().GenerateToken(value)
                });
            }
            return NotFound();
        }
    }
}
