using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace PatientServiceTests.Data.ComponentTests.TestHelpers;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public HttpMessageHandler MessageHandler { get; }

    public TestWebApplicationFactory(HttpMessageHandler messageHandler)
    {
        MessageHandler = messageHandler;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            ServiceCollectionDescriptorExtensions.RemoveAll(services, typeof(HttpClient));
            HttpClientFactoryServiceCollectionExtensions.AddHttpClient(services, "SalveClient", client =>
                {
                    client.BaseAddress = new Uri("http://testaddress.com");
                })
                .ConfigurePrimaryHttpMessageHandler(() => MessageHandler);
        });
        
        base.ConfigureWebHost(builder);
    }
}