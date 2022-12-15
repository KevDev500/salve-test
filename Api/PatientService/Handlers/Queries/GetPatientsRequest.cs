using System.Collections.Generic;
using MediatR;
using PatientService.Models;

namespace PatientService.Handlers.Queries;

public readonly record struct GetPatientsRequest(int ClinicId): IRequest<IEnumerable<Patient>>;