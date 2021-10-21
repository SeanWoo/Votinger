using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Test.TestClient _client;

        public TestController(Test.TestClient client)
        {
            _client = client;
        }

        [HttpGet("{data}")]
        public async Task<string> Get(string data)
        {
            var result = await _client.SayHelloAsync(new HelloRequest() { Name = data });

            return result.Message;
        }
    }
}
