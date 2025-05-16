using KarginScales.Models;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace KarginScales.Service;

public class ExcelDataService : IDataService
{

    public string FilePath { get; }


    public List<DataPoint> LoadDataPoint(string name)
    {
        throw new System.NotImplementedException();
    }

    public List<Polymer> LoadNamesPolymer()
    {
        throw new System.NotImplementedException();
    }
}
