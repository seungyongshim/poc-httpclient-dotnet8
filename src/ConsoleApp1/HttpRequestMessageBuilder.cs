using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

// https://github.com/microsoft/azure-health-data-services-toolkit/blob/master/src/Microsoft.AzureHealth.DataServices.Core/Clients/HttpRequestMessageBuilder.cs#L16

public record HttpRequestMessageBuilder
{
    public HttpContent? Content { get; init; }
    public required Uri Path { get; init; }
    public string? QueryString { get; init; }
    public IEnumerable<IEnumerable<string>> Headers { get; init; } = Array.Empty<string[]>();
    public HttpMethod Method { get; init; } = HttpMethod.Get;

    public HttpRequestMessage Build()
    {
        UriBuilder builder = new(BaseUrl)
        {
            Path = Path,
            Query = QueryString,
        };
        var baseUrl = new Uri(builder.ToString()).AbsoluteUri;
        HttpRequestMessage request = new(Method, baseUrl);
        AddPipelineHeadersToRequest(request, Headers);
        request.Content = Content;

        return request;
    }

    private static void AddPipelineHeadersToRequest(HttpRequestMessage request, NameValueCollection headers)
    {
        if (headers?.AllKeys is not null)
        {
            foreach (var item in headers.AllKeys)
            {
                if (
                    item is not null &&
                    !HttpMessageExtensions.ContentHeaderNames
                        .Any(x => string.Equals(x, item, StringComparison.OrdinalIgnoreCase)))
                {
                    request.Headers.Add(item, headers.Get(item));
                }
            }
        }

        request.Headers.Add("Host", request.RequestUri.Authority);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue(DefaultUserAgentHeader));
    }
}
