using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using PatientService.Helpers.Csv.Maps;

namespace PatientService.Helpers.Csv;

public class CsvHelperWrapper : ICsvHelper
{   
    private readonly IEnumerable<IClassMap> _classMaps;

    public CsvHelperWrapper(IEnumerable<IClassMap> classMaps) => 
        _classMaps = classMaps;

    public IEnumerable<T> GetRecords<T>(StreamReader streamReader)
    {
        using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
        RegisterClassMap<T>(csvReader);
        return csvReader.GetRecords<T>().ToList();
    }

    private void RegisterClassMap<T>(CsvReader csvReader)
    {
        IClassMap classMap = _classMaps.First(m => m.ClassType == typeof(T));
        csvReader.Context.RegisterClassMap(classMap.GetType());
    }
}