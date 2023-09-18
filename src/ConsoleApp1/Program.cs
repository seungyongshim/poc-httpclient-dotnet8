using System.Net.Http.Json;
using ConsoleApp1;

HttpClient httpClient = new()
{

};

var req = new HttpRequestMessageBuilder
{
    Path = "/",
    Content = new StringContent("Ok")
}.Build();

var cts = new CancellationTokenSource();

using var res = await httpClient.SendAsync(new HttpRequestMessage
{
    
}, HttpCompletionOption.ResponseContentRead, cts.Token);


res.Content.LoadIntoBufferAsync
