using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PatientService.Models;
using PatientService.Models.Services;
using PatientServiceTests.Data.ComponentTests.TestHelpers;

namespace PatientServiceTests.Data;

public class ClinicStoreComponentTests
{
    [Fact]
    public async Task GetClinics_CanParseExpectedData()
    {
        CancellationToken cancellationToken = default;
        
        List<Clinic> expectedClinics = new()
        {
            new Clinic(1, "KevinClinic1"),
            new Clinic(2, "KevinClinic2"),
        };

        HttpStatusCode responseStatus = HttpStatusCode.OK;
        string responseContent =
            $"id,name{Environment.NewLine}{string.Join(Environment.NewLine, expectedClinics.Select(c => $"{c.Id},{c.Name}"))}";

        HttpMessageHandlerFake messageHandlerFake = new(responseStatus, responseContent);

        await using TestWebApplicationFactory application = new(messageHandlerFake);

        IClinicStore clinicStore = application.Services.GetRequiredService<IClinicStore>();

        IEnumerable<Clinic> actualClinics = await clinicStore.GetClinics(cancellationToken);

        actualClinics.Should()
            .NotBeEmpty();

        actualClinics.Should().BeEquivalentTo(expectedClinics);
    }
}