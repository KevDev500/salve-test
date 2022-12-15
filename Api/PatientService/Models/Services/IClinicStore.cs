using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PatientService.Models.Services;

public interface IClinicStore
{
    Task<IEnumerable<Clinic>> GetClinics(CancellationToken cancellationToken);
}