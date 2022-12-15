using CsvHelper.Configuration;
using PatientService.Models;

namespace PatientService.Helpers.Csv.Maps;

public class PatientMap : ClassMap<Patient>, IClassMap
{
    public PatientMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.FirstName).Name("first_name");
        Map(m => m.LastName).Name("last_name");
        Map(m => m.DateOfBirth).Name("date_of_birth");
    }
}