using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PatientService.Helpers.Csv;
using PatientService.Models;
using PatientService.Models.Services;

namespace PatientService.Data;

public class ClinicStore : IClinicStore
{
    private readonly HttpClient _httpClient;
    private readonly ICsvHelper _csvHelper;

    public ClinicStore(HttpClient httpClient, ICsvHelper csvHelper)
    {
        _httpClient = httpClient;
        _csvHelper = csvHelper;
    }

    public async Task<IEnumerable<Clinic>> GetClinics(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.GetAsync("clinics.csv", cancellationToken);
        
        response.EnsureSuccessStatusCode();

        await using Stream responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using StreamReader streamReader = new(responseStream);
        
        return _csvHelper.GetRecords<Clinic>(streamReader);
    }
}