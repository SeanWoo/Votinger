using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Gateway.Web.Extensions.ClaimsExtensions;

namespace Votinger.Gateway.Web.Interceptors
{
    public class JwtInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _context;
        public JwtInterceptor(IHttpContextAccessor context)
        {
            _context = context;
        }
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var authorizationHeader = _context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization");
            var metadata = new Metadata();

            if (authorizationHeader.Key is not null)
                metadata.Add("Authorization", authorizationHeader.Value);

            var callOptions = context.Options.WithHeaders(metadata);

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOptions);

            return continuation(request, newContext);
        }
    }
}
