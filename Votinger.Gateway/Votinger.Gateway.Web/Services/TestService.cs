using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Services
{
    public class TestService
    {
        private Test.TestClient _client;
        public TestService(Test.TestClient client)
        {
            _client = client;
        }
        public async Task<string> Execute(string data)
        {
            var result = await _client.SayHelloAsync(new HelloRequest() { Name = data });
            return result.Message;
        }
    }
}
