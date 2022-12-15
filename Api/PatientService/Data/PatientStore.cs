using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PatientService.Helpers.Csv;
using PatientService.Models;
using PatientService.Models.Services;

namespace PatientService.Data;

public class PatientStore : IPatientStore
{
    private readonly HttpClient _httpClient;
    private readonly ICsvHelper _csvHelper;

    public PatientStore(HttpClient httpClient, ICsvHelper csvHelper)
    {
        _httpClient = httpClient;
        _csvHelper = csvHelper;
    }

    public async Task<IEnumerable<Patient>> GetByClinicId(int clinicId, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"patients-{clinicId}.csv", cancellationToken);
        
        response.EnsureSuccessStatusCode();

        await using Stream responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using StreamReader streamReader = new(responseStream);
        
        return _csvHelper.GetRecords<Patient>(streamReader);
    }
}