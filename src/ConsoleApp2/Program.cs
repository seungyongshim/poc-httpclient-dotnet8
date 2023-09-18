using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

var server = WireMockServer.Start();

server.Given(Request.Create()
                    .WithPath("/some/thing")
                    .UsingGet())
      .RespondWith(Response.Create()
                           .WithStatusCode(200)
                           .WithHeader("Content-Type", "text/plain")
                           .WithBody("Hello world!"));

server.CreateClient();
