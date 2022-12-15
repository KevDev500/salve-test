using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PatientService.Handlers.Queries;
using PatientService.Models;
using PatientService.Models.Services;

namespace PatientService.Handlers;

public class GetPatientsRequestHandler : IRequestHandler<GetPatientsRequest, IEnumerable<Patient>>
{
    private readonly IPatientStore _patientStore;
    
    public GetPatientsRequestHandler(IPatientStore patientStore) => _patientStore = patientStore;
    
    public async Task<IEnumerable<Patient>> Handle(GetPatientsRequest request, CancellationToken cancellationToken) =>
        await _patientStore.GetByClinicId(request.ClinicId, cancellationToken);
}