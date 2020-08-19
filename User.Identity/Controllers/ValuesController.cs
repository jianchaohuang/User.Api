using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Identity.Services;

namespace User.Identity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IUserService _userService;
        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var userInfo = _userService.CheckOrCreate("13556996063"); //zipkin跟踪测试
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
