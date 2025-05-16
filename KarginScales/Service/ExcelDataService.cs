using KarginScales.Models;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using System.Linq;

namespace KarginScales.Service;

public class ExcelDataService : IDataService
{
    private readonly XLWorkbook _book;

    public string FilePath { get; }

    public ExcelDataService(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        FilePath = filePath;
        _book = new XLWorkbook(FilePath);
    }

    public List<DataPoint> LoadDataPoint(string name)
    {
        var result = new List<DataPoint>();

        if (!_book.TryGetWorksheet(name, out var ws))
            return result;

        var table = ws.RangeUsed();

        if (table == null)
            return result;

        for(int row = 2; row <= table.RowCount(); ++row)
        {
            result.Add(new DataPoint
            {
                Temperature = table.Cell(row, 1).GetDouble(),
                Gamma = table.Cell(row, 2).GetDouble()
            });
        }

        return result;
    }

    public List<Polymer> LoadNamesPolymer()
    {
        return _book.Worksheets
            .Select(s => new Polymer(s.Name))
            .ToList();
    }
}
