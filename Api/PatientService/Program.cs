using System;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PatientService.Data;
using PatientService.Handlers.Queries;
using PatientService.Helpers.Csv;
using PatientService.Helpers.Csv.Maps;
using PatientService.Models.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddHttpClient("SalveClient", client =>
    {
        client.BaseAddress = client.BaseAddress = new Uri("https://raw.githubusercontent.com/salvehealth/tech-test-data/master/");
    })
    .AddTypedClient<IClinicStore, ClinicStore>()
    .AddTypedClient<IPatientStore, PatientStore>();

services
    .AddSingleton<IClassMap, ClinicMap>()
    .AddSingleton<IClassMap, PatientMap>()
    .AddSingleton<ICsvHelper, CsvHelperWrapper>();

services.AddMediatR(typeof(Program));
services.AddEndpointsApiExplorer();
services.AddCors();
services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin());


app.MapGet("clinics",
    async ([FromServices] IMediator mediator, CancellationToken cancellationToken) =>
        await mediator.Send(new GetClinicsRequest(), cancellationToken));

app.MapGet("clinics/{clinicId:int}/patients",
    async (int clinicId, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
        await mediator.Send(new GetPatientsRequest(clinicId), cancellationToken));

app.Run();

public partial class Program { }