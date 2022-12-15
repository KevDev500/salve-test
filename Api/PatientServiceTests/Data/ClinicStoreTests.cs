using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PatientService.Data;
using PatientService.Helpers.Csv;
using PatientService.Models;

namespace PatientServiceTests.Data;

public class ClinicStoreTests
{
    private readonly ClinicStore _clinicStore;
    private readonly Mock<HttpMessageHandler> _messageHandler;
    private readonly Mock<ICsvHelper> _csvHelper;
    private readonly HttpClient _httpClient;

    public ClinicStoreTests()
    {
        _messageHandler = new Mock<HttpMessageHandler>();
        _csvHelper = new Mock<ICsvHelper>();
        _httpClient = new HttpClient(_messageHandler.Object);
        
        _clinicStore = new ClinicStore(_httpClient, _csvHelper.Object);
    }

    [Fact]
    public async Task GetClinics_IfExternalResourceCannotBeFound_ThrowsExpectedException()
    {
        CancellationToken cancellationToken = default;

        _httpClient.BaseAddress = new Uri("http://test.com");
        
        _messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("mocked API response")
            });

        Func<Task<IEnumerable<Clinic>>> action = () => _clinicStore.GetClinics(cancellationToken);

        await action.Should().ThrowAsync<HttpRequestException>();
    }
}