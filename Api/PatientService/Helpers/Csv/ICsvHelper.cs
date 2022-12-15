using System.Collections.Generic;
using System.IO;

namespace PatientService.Helpers.Csv;

public interface ICsvHelper
{
    IEnumerable<T> GetRecords<T>(StreamReader streamReader);
}