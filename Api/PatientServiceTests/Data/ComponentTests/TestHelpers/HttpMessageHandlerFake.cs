using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PatientServiceTests.Data.ComponentTests.TestHelpers;

public class HttpMessageHandlerFake : HttpMessageHandler
{
    public HttpMessageHandlerFake(HttpStatusCode statusCode, string content)
    {
        StatusCode = statusCode;
        Content = new StringContent(content);
    }
    public HttpStatusCode StatusCode { get; }

    public StringContent Content { get; }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage responseMessage = new(StatusCode);
        responseMessage.Content = Content;

        return await Task.FromResult(responseMessage);
    }
}