using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp
{
    internal class Mocks
    {
        public class FakeHttpMessageHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage _response;
            public FakeHttpMessageHandler(HttpResponseMessage respone) 
            {
                _response = respone;
            }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) 
            {
                return Task.FromResult(_response);
            }
        }
    }
}
