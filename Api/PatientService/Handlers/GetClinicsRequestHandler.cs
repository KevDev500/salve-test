using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PatientService.Handlers.Queries;
using PatientService.Models;
using PatientService.Models.Services;

namespace PatientService.Handlers;

public class GetClinicsRequestHandler : IRequestHandler<GetClinicsRequest, IEnumerable<Clinic>>
{
    private readonly IClinicStore _clinicStore;
    
    public GetClinicsRequestHandler(IClinicStore clinicStore) => _clinicStore = clinicStore;
    
    public async Task<IEnumerable<Clinic>> Handle(GetClinicsRequest request, CancellationToken cancellationToken) =>
        await _clinicStore.GetClinics(cancellationToken);
}