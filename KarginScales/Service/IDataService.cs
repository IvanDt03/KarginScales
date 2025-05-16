using KarginScales.Models;
using System.Collections.Generic;

namespace KarginScales.Service;

public interface IDataService
{
    List<Polymer> LoadNamesPolymer();

    List<DataPoint> LoadDataPoint(string name);
}
