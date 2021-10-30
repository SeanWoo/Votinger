using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.PollServer.Infrastructure.Repository;
using Votinger.Protos;

namespace Votinger.PollServer.Web.GrpcServices
{
    public class TestService : Test.TestBase
    {
        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return new HelloReply()
            {
                Message = "Hello " + request.Name
            };
        }
    }
}
