using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PatientService.Models.Services;

public interface IPatientStore
{
    Task<IEnumerable<Patient>> GetByClinicId(int requestClinicId, CancellationToken cancellationToken);
}