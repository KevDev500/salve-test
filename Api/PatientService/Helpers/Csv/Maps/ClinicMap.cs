using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using PatientService.Models;

namespace PatientService.Helpers.Csv.Maps;

public class ClinicMap : ClassMap<Clinic>, IClassMap
{
    public ClinicMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.Name).Name("name");
    }
}